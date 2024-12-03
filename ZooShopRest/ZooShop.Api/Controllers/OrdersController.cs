using MediatR;
using Microsoft.AspNetCore.Mvc;
using ZooShop.Application.Orders.Commands;
using ZooShop.Application.Orders.Exceptions;
using ZooShop.Domain.Orders;

namespace ZooShop.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder(CreateOrderCommand command)
    {
        var result = await _mediator.Send(command);

        return result.Match<IActionResult>(
            order => Ok(order),
            exception => exception switch
            {
                OrderNotFoundException => NotFound(exception.Message),
                _ => StatusCode(500, exception.Message)
            });
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateOrder(Guid id, UpdateOrderCommand command)
    {
        if (id != command.OrderId)
        {
            return BadRequest("Order ID mismatch");
        }

        var result = await _mediator.Send(command);

        return result.Match<IActionResult>(
            order => Ok(order),
            exception => exception switch
            {
                OrderNotFoundException => NotFound(exception.Message),
                _ => StatusCode(500, exception.Message)
            });
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteOrder(Guid id)
    {
        var command = new DeleteOrderCommand { OrderId = id };
        var result = await _mediator.Send(command);

        return result.Match<IActionResult>(
            order => Ok(order),
            exception => exception switch
            {
                OrderNotFoundException => NotFound(exception.Message),
                _ => StatusCode(500, exception.Message)
            });
    }
}