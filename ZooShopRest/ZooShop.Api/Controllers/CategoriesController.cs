using MediatR;
using Microsoft.AspNetCore.Mvc;
using ZooShop.Application.Сategorys.Commands;
using ZooShop.Application.Сategorys.Exceptions;
using ZooShop.Domain.Categorys;

namespace ZooShop.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CategoriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory(CreateCategoryCommand command)
    {
        var result = await _mediator.Send(command);

        return result.Match<IActionResult>(
            category => Ok(category),
            exception => exception switch
            {
                CategoryAlreadyExistsException => Conflict(exception.Message),
                _ => StatusCode(500, exception.Message)
            });
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateCategory(Guid id, UpdateCategoryCommand command)
    {
        if (id != command.CategoryId)
        {
            return BadRequest("Category ID mismatch");
        }

        var result = await _mediator.Send(command);

        return result.Match<IActionResult>(
            category => Ok(category),
            exception => exception switch
            {
                CategoryNotFoundException => NotFound(exception.Message),
                _ => StatusCode(500, exception.Message)
            });
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteCategory(Guid id)
    {
        var command = new DeleteCategoryCommand { CategoryId = id };
        var result = await _mediator.Send(command);

        return result.Match<IActionResult>(
            category => Ok(category),
            exception => exception switch
            {
                CategoryNotFoundException => NotFound(exception.Message),
                _ => StatusCode(500, exception.Message)
            });
    }
}