using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging;
using Plant.Abstractions;
using Plant.Exceptions;

namespace Plant.AspNetCore;

public class StandardExceptionFilterAttribute : ExceptionFilterAttribute
{
    private readonly ProblemDetailsFactory _problemDetailsFactory;
    private readonly ILogger<StandardExceptionFilterAttribute> _logger;

    public StandardExceptionFilterAttribute(
        ProblemDetailsFactory problemDetailsFactory,
        ILogger<StandardExceptionFilterAttribute> logger)
    {
        _problemDetailsFactory = problemDetailsFactory;
        _logger = logger;
    }

    public override void OnException(ExceptionContext context)
    {
        ProblemDetails problemDetails;

        if (context.Exception is StandardException exception)
        {
            _logger.LogError(context.Exception, PlantConstants.Errors.StandardExceptionOccurred);

            context.HttpContext.Items[PlantConstants.Errors.ProblemDetailsErrors] = new Dictionary<string, string[]>
            {
                { exception.Code, new string[] { exception.DetailMessage ?? PlantConstants.Errors.DetailMessage } }
            };

            problemDetails = _problemDetailsFactory.CreateProblemDetails(
                context.HttpContext,
                statusCode: exception.StatusCode,
                detail: exception.DetailMessage ?? PlantConstants.Errors.DetailMessage);
        }
        else
        {
            _logger.LogError(context.Exception, PlantConstants.Errors.ExceptionOccurred);

            problemDetails = _problemDetailsFactory.CreateProblemDetails(
                context.HttpContext,
                statusCode: 500);
        }

        context.Result = new ObjectResult(problemDetails);
        context.ExceptionHandled = true;
    }
}
