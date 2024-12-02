using ZooShop.Application.Common;
using ZooShop.Application.Common.Interfaces.Repositories;
using ZooShop.Application.Orders.Exceptions;
using ZooShop.Domain.Orders;
using MediatR;

namespace ZooShop.Application.Orders.Commands;

public record DeleteOrderCommand : IRequest<Result<Order, OrderException>>
{
    public required Guid OrderId { get; init; }
}

public class DeleteOrderCommandHandler(IOrderRepository orderRepository)
    : IRequestHandler<DeleteOrderCommand, Result<Order, OrderException>>
{
    public async Task<Result<Order, OrderException>> Handle(
        DeleteOrderCommand request,
        CancellationToken cancellationToken)
    {
        var orderId = new OrderId(request.OrderId);

        var existingOrder = await orderRepository.GetById(orderId, cancellationToken);

        return await existingOrder.Match<Task<Result<Order, OrderException>>>(
            async order => await DeleteEntity(order, cancellationToken),
            () => Task.FromResult<Result<Order, OrderException>>(new OrderNotFoundException(orderId))
        );
    }

    private async Task<Result<Order, OrderException>> DeleteEntity(Order order, CancellationToken cancellationToken)
    {
        try
        {
            return await orderRepository.Delete(order, cancellationToken);
        }
        catch (Exception exception)
        {
            return new OrderUnknownException(order.Id, exception);
        }
    }
}