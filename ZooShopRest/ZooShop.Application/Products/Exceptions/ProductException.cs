using ZooShop.Domain.Products;

namespace ZooShop.Application.Products.Exceptions;

public abstract class ProductException(ProductId id, string message, Exception? innerException = null)
    : Exception(message, innerException)
{
    public ProductId ProductId { get; } = id;
}

public class ProductNotFoundException(ProductId id) 
    : ProductException(id, $"Product under id: {id} not found");

public class ProductAlreadyExistsException(ProductId id) 
    : ProductException(id, $"Product already exists: {id}");

public class ProductUnknownException(ProductId id, Exception innerException)
    : ProductException(id, $"Unknown exception for the product under id: {id}", innerException);
