using AuthApi.Communication.Responses;
using AuthApi.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AuthApi.Api.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is BaseException)
        {
            HandleProjectException(context);
            return;
        }
        ThrowUnknownException(context);
    }

    private void ThrowUnknownException(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(new ErrorResponse("Erro interno do servidor"));
    }

    private void HandleProjectException(ExceptionContext context)
    {
        if (context.Exception is ErrorOnValidationException)
            context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

        if (context.Exception is ConflictException)
            context.HttpContext.Response.StatusCode = StatusCodes.Status409Conflict;

        if (context.Exception is NotFoundException)
            context.HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;

        context.Result = new ObjectResult(new ErrorResponse(context.Exception.Message));
    }
}
