using Microsoft.AspNetCore.Mvc;
using ZooShop.Application.Сategorys.Exceptions;
namespace ZooShop.Api.Modules.Errors;

public static class CategoriesErrorHandler
{
    public static ObjectResult ToObjectResult(this CategoryException exception)
    {
        return new ObjectResult(exception.Message)
        {
            StatusCode = exception switch
            {
                CategoryNotFoundException => StatusCodes.Status404NotFound,
                CategoryAlreadyExistsException => StatusCodes.Status409Conflict,
                CategoryUnknownException => StatusCodes.Status500InternalServerError,
                _ => throw new NotImplementedException("Category error handler does not implemented")
            }
        };
    }
}