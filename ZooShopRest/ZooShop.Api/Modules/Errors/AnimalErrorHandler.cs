using Microsoft.AspNetCore.Mvc;
using ZooShop.Application.Animals.Exceptions;
using ZooShop.Application.Customers.Exceptions;

namespace ZooShop.Api.Modules.Errors;

public static class AnimalErrorHandler
{
    public static ObjectResult ToObjectResult(this AnimalException exception)
    {
        return new ObjectResult(exception.Message)
        {
            StatusCode = exception switch
            {
                AnimalNotFoundException => StatusCodes.Status404NotFound,
                AnimalAlreadyExistsException => StatusCodes.Status409Conflict,
                AnimalUnknownException => StatusCodes.Status500InternalServerError,
                _ => throw new NotImplementedException("Animal error handler does not implemented")
            }
        };
    }
}