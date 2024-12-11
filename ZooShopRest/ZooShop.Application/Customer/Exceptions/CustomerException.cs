using ZooShop.Domain.Customers;
namespace ZooShop.Application.Customers.Exceptions;
public abstract class CustomerException(CustomerId id, string message, Exception? innerException = null)
    : Exception(message, innerException)
{
    public CustomerId CustomerId { get; } = id;
}
public class CustomerNotFoundException(CustomerId id) 
    : CustomerException(id, $"Customer under id: {id} not found");
public class CustomerAlreadyExistsException(CustomerId id) 
    : CustomerException(id, $"Customer already exists: {id}");
public class CustomerUnknownException(CustomerId id, Exception innerException)
    : CustomerException(id, $"Unknown exception for the customer under id: {id}", innerException);