using ZooShop.Application.Common;
using ZooShop.Application.Common.Interfaces.Repositories;
using ZooShop.Application.Orders.Exceptions;
using ZooShop.Domain.Orders;
using MediatR;
using Optional;
using Optional.Unsafe;
using ZooShop.Domain.Products;
using ZooShop.Domain.Customers;

namespace ZooShop.Application.Orders.Commands;

public record CreateOrderCommand : IRequest<Result<Order, OrderException>>
{
    public required Guid CustomerId { get; init; }
    public required List<Guid> ProductIds { get; init; }
    public required decimal TotalAmount { get; init; }
}

public class CreateOrderCommandHandler(IOrderRepository orderRepository, IProductRepository productRepository) 
    : IRequestHandler<CreateOrderCommand, Result<Order, OrderException>>
{
    public async Task<Result<Order, OrderException>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var orderId = OrderId.New();
        var order = Order.New(orderId, request.TotalAmount);

        foreach (var productId in request.ProductIds)
        {
            var product = await productRepository.GetById(new ProductId(productId), cancellationToken);
            if (product.HasValue)
            {
                order.AddProduct(product.ValueOrFailure());
            }
            else
            {
                return Result<Order, OrderException>.Failure(new OrderNotFoundException(orderId));
            }
        }

        try
        {
            return await orderRepository.Add(order, cancellationToken);
        }
        catch (Exception exception)
        {
            return new OrderUnknownException(orderId, exception);
        }
    }
}