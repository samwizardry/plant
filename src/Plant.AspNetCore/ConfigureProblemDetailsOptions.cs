using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Plant.Abstractions;

namespace Plant.AspNetCore;

public class ConfigureProblemDetailsOptions : IConfigureOptions<ProblemDetailsOptions>
{
    public void Configure(ProblemDetailsOptions options)
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
    }
}
