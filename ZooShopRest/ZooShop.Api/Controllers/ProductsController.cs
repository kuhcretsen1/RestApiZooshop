using ZooShop.Api.Dtos;
using ZooShop.Api.Modules.Errors;
using ZooShop.Application.Common.Interfaces.Queries;
using ZooShop.Application.Products.Commands;
using ZooShop.Domain.Products;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ZooShop.Domain.Categorys;


namespace Api.Controllers;

[Route("products")]
[ApiController]
public class ProductsController(ISender sender, IProductQueries productQueries) : ControllerBase
{
   

    [HttpGet("{productId:guid}")]
    public async Task<ActionResult<ProductDto>> Get([FromRoute] Guid productId, CancellationToken cancellationToken)
    {
        var entity = await productQueries.GetById(new ProductId(productId), cancellationToken);
        return entity.Match<ActionResult<ProductDto>>(
            u => ProductDto.FromDomainModel(u),
            () => NotFound());
    }

    [HttpPost]
    public async Task<ActionResult<ProductDto>> Create([FromBody] ProductDto request, CancellationToken cancellationToken)
    {
        var input = new CreateProductCommand
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            StockQuantity = request.StockQuantity,
            CategoryId = new CategoryId(request.CategoryId)
        };

        var result = await sender.Send(input, cancellationToken);
        return result.Match<ActionResult<ProductDto>>(
            c => ProductDto.FromDomainModel(c),
            e => e.ToObjectResult());
    }

    [HttpPut]
    public async Task<ActionResult<ProductDto>> Update([FromBody] ProductDto request, CancellationToken cancellationToken)
    {
        var input = new UpdateProductCommand
        {
            ProductId = request.Id!.Value,
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            StockQuantity = request.StockQuantity
        };

        var result = await sender.Send(input, cancellationToken);
        return result.Match<ActionResult<ProductDto>>(
            f => ProductDto.FromDomainModel(f),
            e => e.ToObjectResult());
    }

    [HttpDelete("{productId:guid}")]
    public async Task<ActionResult<ProductDto>> Delete([FromRoute] Guid productId, CancellationToken cancellationToken)
    {
        var input = new DeleteProductCommand
        {
            ProductId = productId
        };

        var result = await sender.Send(input, cancellationToken);
        return result.Match<ActionResult<ProductDto>>(
            c => ProductDto.FromDomainModel(c),
            e => e.ToObjectResult());
    }
}