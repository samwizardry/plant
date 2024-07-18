using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Plant.Errors;

namespace Plant;

public abstract class PlantControllerBase : ControllerBase
{
    [NonAction]
    protected IActionResult Problem(IEnumerable<Error> errors)
    {
        HttpContext.Items["errors"] = errors
            .GroupBy(p => p.Code)
            .ToDictionary(g => g.Key, g => g.Select(p => p.Description).ToArray());

        return Problem(statusCode: errors.First().StatusCode);
    }

    [NonAction]
    protected IActionResult Problem(Error error)
    {
        HttpContext.Items["errors"] = new Dictionary<string, string[]>
        {
            { error.Code, new string[] { error.Description } }
        };

        return Problem(statusCode: error.StatusCode);
    }
}
