using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Plant.Abstractions;
using Plant.Errors;

namespace Plant.AspNetCore;

public abstract class PlantControllerBase : ControllerBase
{
    [NonAction]
    protected IActionResult Problem(IEnumerable<Error> errors)
    {
        HttpContext.Items[PlantConstants.Errors.ProblemDetailsErrors] = errors
            .GroupBy(p => p.Code)
            .ToDictionary(g => g.Key, g => g.Select(p => p.Description).ToArray());

        return Problem(statusCode: errors.OrderByDescending(e => e.StatusCode).First().StatusCode);
    }

    [NonAction]
    protected IActionResult Problem(Error error)
    {
        HttpContext.Items[PlantConstants.Errors.ProblemDetailsErrors] = new Dictionary<string, string[]>
        {
            { error.Code, new string[] { error.Description } }
        };

        return Problem(statusCode: error.StatusCode);
    }
}
