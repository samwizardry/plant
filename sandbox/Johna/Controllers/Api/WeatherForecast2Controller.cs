using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Plant.AspNetCore;

namespace Johna.Controllers;

[ApiVersion(2.0d)]
[ControllerName("weatherforecast")]
public class WeatherForecast2Controller : ApiControllerBase
{
    private readonly ILogger<WeatherForecast2Controller> _logger;

    public WeatherForecast2Controller(ILogger<WeatherForecast2Controller> logger)
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
