using ZooShop.Application.Products.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace ZooShop.Api.Modules.Errors;

public static class ProductErrorHandler
{
    public static ObjectResult ToObjectResult(this ProductException exception)
    {
        return new ObjectResult(exception.Message)
        {
            StatusCode = exception switch
            {
                ProductNotFoundException => StatusCodes.Status404NotFound,
                ProductAlreadyExistsException => StatusCodes.Status409Conflict,
                ProductUnknownException => StatusCodes.Status500InternalServerError,
                _ => throw new NotImplementedException("Product error handler does not implemented")
            }
        };
    }
}