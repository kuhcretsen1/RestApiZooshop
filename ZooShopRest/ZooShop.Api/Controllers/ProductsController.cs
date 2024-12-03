using MediatR;
using Microsoft.AspNetCore.Mvc;
using ZooShop.Application.Products.Commands;
using ZooShop.Application.Products.Exceptions;
using ZooShop.Domain.Products;

namespace ZooShop.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(CreateProductCommand command)
    {
        var result = await _mediator.Send(command);

        return result.Match<IActionResult>(
            product => Ok(product),
            exception => exception switch
            {
                ProductAlreadyExistsException => Conflict(exception.Message),
                _ => StatusCode(500, exception.Message)
            });
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateProduct(Guid id, UpdateProductCommand command)
    {
        if (id != command.ProductId)
        {
            return BadRequest("Product ID mismatch");
        }

        var result = await _mediator.Send(command);

        return result.Match<IActionResult>(
            product => Ok(product),
            exception => exception switch
            {
                ProductNotFoundException => NotFound(exception.Message),
                _ => StatusCode(500, exception.Message)
            });
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        var command = new DeleteProductCommand { ProductId = id };
        var result = await _mediator.Send(command);

        return result.Match<IActionResult>(
            product => Ok(product),
            exception => exception switch
            {
                ProductNotFoundException => NotFound(exception.Message),
                _ => StatusCode(500, exception.Message)
            });
    }
}