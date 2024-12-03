using MediatR;
using Microsoft.AspNetCore.Mvc;
using ZooShop.Application.Customers.Commands;
using ZooShop.Application.Customers.Exceptions;
using ZooShop.Domain.Customers;

namespace ZooShop.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly IMediator _mediator;

    public CustomerController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCustomer(CreateCustomerCommand command)
    {
        var result = await _mediator.Send(command);

        return result.Match<IActionResult>(
            customer => Ok(customer),
            exception => exception switch
            {
                CustomerAlreadyExistsException => Conflict(exception.Message),
                _ => StatusCode(500, exception.Message)
            });
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateCustomer(Guid id, UpdateCustomerCommand command)
    {
        if (id != command.CustomerId)
        {
            return BadRequest("Customer ID mismatch");
        }

        var result = await _mediator.Send(command);

        return result.Match<IActionResult>(
            customer => Ok(customer),
            exception => exception switch
            {
                CustomerNotFoundException => NotFound(exception.Message),
                _ => StatusCode(500, exception.Message)
            });
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteCustomer(Guid id)
    {
        var command = new DeleteCustomerCommand { CustomerId = id };
        var result = await _mediator.Send(command);

        return result.Match<IActionResult>(
            customer => Ok(customer),
            exception => exception switch
            {
                CustomerNotFoundException => NotFound(exception.Message),
                _ => StatusCode(500, exception.Message)
            });
    }
}