using ZooShop.Api.Dtos;
using ZooShop.Api.Modules.Errors;
using ZooShop.Application.Common.Interfaces.Queries;
using ZooShop.Application.Customers.Commands;
using ZooShop.Domain.Customers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ZooShop.Application.Customers.Exceptions;
using ZooShop.Api.Dtos;
using ZooShop.Api.Modules.Errors;
using ZooShop.Application.Common.Interfaces.Queries;
using ZooShop.Application.Customers.Commands;
using ZooShop.Domain.Customers;

namespace ZooShop.Api.Controllers;

[Route("customers")]
[ApiController]
public class CustomersController(ISender sender, ICustomerQueries customerQueries) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<CustomerDto>>> GetAll(CancellationToken cancellationToken)
    {
        var entities = await customerQueries.GetAll(cancellationToken);
        return entities.Select(CustomerDto.FromDomainModel).ToList();
    }

    [HttpGet("{customerId:guid}")]
    public async Task<ActionResult<CustomerDto>> Get([FromRoute] Guid customerId, CancellationToken cancellationToken)
    {
        var entity = await customerQueries.GetById(new CustomerId(customerId), cancellationToken);
        return entity.Match<ActionResult<CustomerDto>>(
            u => CustomerDto.FromDomainModel(u),
            () => NotFound());
    }

    [HttpPost]
    public async Task<ActionResult<CustomerDto>> Create([FromBody] CustomerDto request, CancellationToken cancellationToken)
    {
        var input = new CreateCustomerCommand
        {
            Name = request.Name,
            Email = request.Email,
            Address = request.Address,
            PhoneNumber = request.PhoneNumber
        };

        var result = await sender.Send(input, cancellationToken);
        return result.Match<ActionResult<CustomerDto>>(
            c => CustomerDto.FromDomainModel(c),
            e => e.ToObjectResult());
    }

    [HttpPut]
    public async Task<ActionResult<CustomerDto>> Update([FromBody] CustomerDto request, CancellationToken cancellationToken)
    {
        var input = new UpdateCustomerCommand
        {
            CustomerId = request.Id!.Value,
            Name = request.Name,
            Email = request.Email,
            Address = request.Address,
            PhoneNumber = request.PhoneNumber
        };

        var result = await sender.Send(input, cancellationToken);
        return result.Match<ActionResult<CustomerDto>>(
            f => CustomerDto.FromDomainModel(f),
            e => e.ToObjectResult());
    }

    [HttpDelete("{customerId:guid}")]
    public async Task<ActionResult<CustomerDto>> Delete([FromRoute] Guid customerId, CancellationToken cancellationToken)
    {
        var input = new DeleteCustomerCommand
        {
            CustomerId = customerId
        };

        var result = await sender.Send(input, cancellationToken);
        return result.Match<ActionResult<CustomerDto>>(
            c => CustomerDto.FromDomainModel(c),
            e => e.ToObjectResult());
    }
}