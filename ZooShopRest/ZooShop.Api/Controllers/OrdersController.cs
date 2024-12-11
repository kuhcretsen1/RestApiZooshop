using ZooShop.Api.Dtos;
using ZooShop.Api.Modules.Errors;
using ZooShop.Application.Common.Interfaces.Queries;
using ZooShop.Application.Orders.Commands;
using ZooShop.Domain.Orders;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("orders")]
[ApiController]
public class OrdersController(ISender sender, IOrderQueries orderQueries) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<OrderDto>>> GetAll(CancellationToken cancellationToken)
    {
        var entities = await orderQueries.GetAll(cancellationToken);
        return entities.Select(OrderDto.FromDomainModel).ToList();
    }

    [HttpGet("{orderId:guid}")]
    public async Task<ActionResult<OrderDto>> Get([FromRoute] Guid orderId, CancellationToken cancellationToken)
    {
        var entity = await orderQueries.GetById(new OrderId(orderId), cancellationToken);
        return entity.Match<ActionResult<OrderDto>>(
            u => OrderDto.FromDomainModel(u),
            () => NotFound());
    }

    [HttpPost]
    [HttpPost]
    public async Task<ActionResult<OrderDto>> Create([FromBody] OrderDto request, CancellationToken cancellationToken)
    {
        var input = new CreateOrderCommand
        {
            CustomerId = Guid.Empty, 
            ProductIds = request.Products.Select(p => p.Id!.Value).ToList(),
            TotalAmount = request.TotalAmount
        };

        var result = await sender.Send(input, cancellationToken);
        return result.Match<ActionResult<OrderDto>>(
            c => OrderDto.FromDomainModel(c),
            e => e.ToObjectResult());
    }

    [HttpPut]
    public async Task<ActionResult<OrderDto>> Update([FromBody] OrderDto request, CancellationToken cancellationToken)
    {
        var input = new UpdateOrderCommand
        {
            OrderId = request.Id!.Value,
            TotalAmount = request.TotalAmount,
            ProductIds = request.Products.Select(p => p.Id!.Value).ToList()
        };

        var result = await sender.Send(input, cancellationToken);
        return result.Match<ActionResult<OrderDto>>(
            f => OrderDto.FromDomainModel(f),
            e => e.ToObjectResult());
    }

    [HttpDelete("{orderId:guid}")]
    public async Task<ActionResult<OrderDto>> Delete([FromRoute] Guid orderId, CancellationToken cancellationToken)
    {
        var input = new DeleteOrderCommand
        {
            OrderId = orderId
        };

        var result = await sender.Send(input, cancellationToken);
        return result.Match<ActionResult<OrderDto>>(
            c => OrderDto.FromDomainModel(c),
            e => e.ToObjectResult());
    }
}