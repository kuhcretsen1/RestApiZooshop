using ZooShop.Application.Common;
using ZooShop.Application.Common.Interfaces.Repositories;
using ZooShop.Application.Products.Exceptions;
using ZooShop.Domain.Products;
using MediatR;
using Optional;

namespace ZooShop.Application.Products.Commands;

public record UpdateProductCommand : IRequest<Result<Product, ProductException>>
{
    public required Guid ProductId { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required decimal Price { get; init; }
    public required int StockQuantity { get; init; }
}

public class UpdateProductCommandHandler(IProductRepository productRepository)
    : IRequestHandler<UpdateProductCommand, Result<Product, ProductException>>
{
    public async Task<Result<Product, ProductException>> Handle(
        UpdateProductCommand request,
        CancellationToken cancellationToken)
    {
        var productId = new ProductId(request.ProductId);
        var product = await productRepository.GetById(productId, cancellationToken);

        return await product.Match(
            async p =>
            {
                var existingProduct = await CheckDuplicated(productId, request.Name, cancellationToken);

                return await existingProduct.Match(
                    ep => Task.FromResult<Result<Product, ProductException>>(new ProductAlreadyExistsException(ep.Id)),
                    async () => await UpdateEntity(p, request, cancellationToken));
            },
            () => Task.FromResult<Result<Product, ProductException>>(new ProductNotFoundException(productId)));
    }

    private async Task<Result<Product, ProductException>> UpdateEntity(
        Product product,
        UpdateProductCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            product.UpdateDetails(request.Name, request.Description, request.Price, request.StockQuantity);

            return await productRepository.Update(product, cancellationToken);
        }
        catch (Exception exception)
        {
            return new ProductUnknownException(product.Id, exception);
        }
    }

    private async Task<Option<Product>> CheckDuplicated(
        ProductId productId,
        string name,
        CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByName(name, cancellationToken);

        return product.Match(
            p => p.Id == productId ? Option.None<Product>() : Option.Some(p),
            Option.None<Product>);
    }
}