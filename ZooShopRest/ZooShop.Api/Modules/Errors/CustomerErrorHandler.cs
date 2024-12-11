using Microsoft.AspNetCore.Mvc;
using ZooShop.Application.Customers.Exceptions;

namespace ZooShop.Api.Modules.Errors;

public static class CustomerErrorHandler
{
    public static ObjectResult ToObjectResult(this CustomerException exception)
    {
        return new ObjectResult(exception.Message)
        {
            StatusCode = exception switch
            {
                CustomerNotFoundException => StatusCodes.Status404NotFound,
                CustomerAlreadyExistsException => StatusCodes.Status409Conflict,
                CustomerUnknownException => StatusCodes.Status500InternalServerError,
                _ => throw new NotImplementedException("Customer error handler does not implemented")
            }
        };
    }
}