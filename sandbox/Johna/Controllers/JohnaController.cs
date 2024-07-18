using System.ComponentModel;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace Johna.Controllers;

[ControllerName("johna")]
public class JohnaController : ApiControllerBase
{
    /// <summary>
    /// Некоторый GET метод из контроллера Johna.
    /// </summary>
    /// <param name="rciCat">Некоторая категория.</param>
    /// <returns></returns>
    [HttpGet]
    public IActionResult Get([FromQuery(Name = "cat")] string? rciCat = "Model X")
    {
        return Ok(ConvertRciCat(rciCat));
    }

    [HttpPost("dt_t_only_test")]
    public IActionResult PostSome([FromQuery] DateOnly someDate, TimeOnly someTime)
    {
        return Ok(new
        {
            someDate,
            someTime
        });
    }

    private string ConvertRciCat(string? rciCat) => rciCat?.ToUpper() switch
    {
        "E" => "0",
        "A" => "1",
        "B" => "2",
        "C" => "3",
        _ => "-"
    };
}
