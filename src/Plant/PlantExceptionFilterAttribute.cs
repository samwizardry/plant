using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging;
using Plant.Abstractions;
using Plant.Exceptions;

namespace Plant;

public class PlantExceptionFilterAttribute : ExceptionFilterAttribute
{
    private readonly ProblemDetailsFactory _problemDetailsFactory;
    private readonly ILogger<PlantExceptionFilterAttribute> _logger;

    public PlantExceptionFilterAttribute(
        ProblemDetailsFactory problemDetailsFactory,
        ILogger<PlantExceptionFilterAttribute> logger)
    {
        _problemDetailsFactory = problemDetailsFactory;
        _logger = logger;
    }

    public override void OnException(ExceptionContext context)
    {
        ProblemDetails problemDetails;

        _logger.LogError(context.Exception.Message);

        if (context.Exception is PlantException exception)
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
