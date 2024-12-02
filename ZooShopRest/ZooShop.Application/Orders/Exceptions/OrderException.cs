using ZooShop.Domain.Orders;

namespace ZooShop.Application.Orders.Exceptions;

public abstract class OrderException(OrderId id, string message, Exception? innerException = null)
    : Exception(message, innerException)
{
    public OrderId OrderId { get; } = id;
}

public class OrderNotFoundException(OrderId id) 
    : OrderException(id, $"Order under id: {id} not found");

public class OrderAlreadyExistsException(OrderId id) 
    : OrderException(id, $"Order already exists: {id}");

public class OrderUnknownException(OrderId id, Exception innerException)
    : OrderException(id, $"Unknown exception for the order under id: {id}", innerException);