using Microsoft.AspNetCore.Http;
using Plant.Abstractions;

namespace Plant.AspNetCore;

public static partial class ProblemDetailsHelper
{
    public static Action<ProblemDetailsOptions> ConfigureProblemDetailsOptions { get; } = new(options =>
    {
        options.CustomizeProblemDetails = ctx =>
        {
            var httpContext = ctx.HttpContext;
            var problemDetails = ctx.ProblemDetails;

            if (httpContext?.Items[PlantConstants.Errors.ProblemDetailsErrors] is object errors)
            {
                problemDetails.Extensions[PlantConstants.Errors.ProblemDetailsErrors] = errors;
            }
        };
    });
}
