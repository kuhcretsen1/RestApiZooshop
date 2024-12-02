using ZooShop.Application.Common;
using ZooShop.Application.Common.Interfaces.Repositories;
using ZooShop.Domain.Products;
using ZooShop.Application.Products.Exceptions;
using MediatR;
using Optional;
using ZooShop.Domain.Categorys;

namespace ZooShop.Application.Products.Commands;

public record CreateProductCommand : IRequest<Result<Product, ProductException>>
{
    public required string Name { get; init; }
    public required decimal Price { get; init; }
    public required string Description { get; init; }
    public required CategoryId CategoryId { get; init; }
    public int StockQuantity { get; init; } = 0;
}

public class CreateProductCommandHandler(
    IProductRepository productRepository) : IRequestHandler<CreateProductCommand, Result<Product, ProductException>>
{
    public async Task<Result<Product, ProductException>> Handle(
        CreateProductCommand request,
        CancellationToken cancellationToken)
    {
        // Перевірка на існування продукту
        var existingProduct = await productRepository.GetByName(request.Name, cancellationToken);

        return await existingProduct.Match(
            p => Task.FromResult<Result<Product, ProductException>>(new ProductAlreadyExistsException(p.Id)),
            async () => await CreateEntity(request, cancellationToken));
    }

    private async Task<Result<Product, ProductException>> CreateEntity(
        CreateProductCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var entity = Product.New(
                id: ProductId.New(), 
                name: request.Name,
                description: request.Description,
                price: request.Price,
                stockQuantity: request.StockQuantity,
                categoryId: request.CategoryId
            );

            return await productRepository.Add(entity, cancellationToken);
        }
        catch (Exception exception)
        {
            return new ProductUnknownException(ProductId.Empty(), exception);
        }
    }
}
