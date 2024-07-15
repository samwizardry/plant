using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Plant.Exceptions;

public class BaseExceptionFilterAttribute : ExceptionFilterAttribute
{
    private readonly ProblemDetailsFactory _problemDetailsFactory;

    public BaseExceptionFilterAttribute(ProblemDetailsFactory problemDetailsFactory)
    {
        _problemDetailsFactory = problemDetailsFactory;
    }

    public override void OnException(ExceptionContext context)
    {
        ProblemDetails problemDetails;

        // TODO: log exception

        if (context.Exception is BaseException exception)
        {
            context.HttpContext.Items[Constants.Errors] = new Dictionary<string, string[]>
            {
                { exception.Code, new string[] { exception.DetailMessage ?? exception.Message } }
            };

            problemDetails = _problemDetailsFactory.CreateProblemDetails(
                context.HttpContext,
                statusCode: exception.StatusCode,
                detail: exception.DetailMessage ?? exception.Message);
        }
        else
        {
            context.HttpContext.Items[Constants.Errors] = new Dictionary<string, string?[]>
            {
                { context.Exception.GetType().ToString(), new string[] { context.Exception.Message } }
            };

            problemDetails = _problemDetailsFactory.CreateProblemDetails(
                context.HttpContext,
                statusCode: 500,
                detail: context.Exception.Message);
        }

        context.Result = new ObjectResult(problemDetails);
        context.ExceptionHandled = true;
    }
}
