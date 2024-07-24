using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace Johna.Controllers;

[ApiVersion(1.0d)]
[ControllerName("weatherforecast")]
public class WeatherForecastController : ApiControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Get(ApiVersion apiVersion)
    {
        _logger.LogInformation("Version {apiVersion}", apiVersion.ToString());

        Random rng = new((int)DateTimeOffset.UtcNow.UtcTicks);

        return Ok(Enumerable.Range(0, 5).Select(_ => new
        {
            Temperature = rng.Next(-15, 5)
        }));
    }
}
