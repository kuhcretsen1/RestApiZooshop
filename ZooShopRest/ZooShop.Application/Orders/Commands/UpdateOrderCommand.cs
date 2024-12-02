using ZooShop.Application.Common;
using ZooShop.Application.Common.Interfaces.Repositories;
using ZooShop.Application.Orders.Exceptions;
using ZooShop.Domain.Orders;
using MediatR;
using Optional;
using Optional.Unsafe;
using ZooShop.Domain.Products;

namespace ZooShop.Application.Orders.Commands;

public record UpdateOrderCommand : IRequest<Result<Order, OrderException>>
{
    public required Guid OrderId { get; init; }
    public required decimal TotalAmount { get; init; }
    public required List<Guid> ProductIds { get; init; }
}

public class UpdateOrderCommandHandler(IOrderRepository orderRepository, IProductRepository productRepository)
    : IRequestHandler<UpdateOrderCommand, Result<Order, OrderException>>
{
    public async Task<Result<Order, OrderException>> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var orderId = new OrderId(request.OrderId);
        var order = await orderRepository.GetById(orderId, cancellationToken);

        return await order.Match(
            async o =>
            {
                return await UpdateEntity(o, request.TotalAmount, request.ProductIds, cancellationToken);
            },
            () => Task.FromResult<Result<Order, OrderException>>(new OrderNotFoundException(orderId)));
    }

    private async Task<Result<Order, OrderException>> UpdateEntity(
        Order order,
        decimal totalAmount,
        List<Guid> productIds,
        CancellationToken cancellationToken)
    {
        try
        {
            order.UpdateTotalAmount(totalAmount);

            foreach (var productId in productIds)
            {
                var product = await productRepository.GetById(new ProductId(productId), cancellationToken);
                if (product.HasValue)
                {
                    order.AddProduct(product.ValueOrFailure());
                }
                else
                {
                    return Result<Order, OrderException>.Failure(new OrderNotFoundException(order.Id));
                }
            }

            return await orderRepository.Update(order, cancellationToken);
        }
        catch (Exception exception)
        {
            return new OrderUnknownException(order.Id, exception);
        }
    }
}