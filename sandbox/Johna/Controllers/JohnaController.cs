using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Plant.Errors;
using Plant.Exceptions;

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

    [HttpGet("error")]
    public IActionResult GetError()
    {
        return Problem(Error.Forbidden());
    }

    [HttpGet("exception")]
    public IActionResult GetException()
    {
        throw new NotImplementedException("Тестовый exception.");
    }

    [HttpGet("standard-exception")]
    public IActionResult GetStandardException()
    {
        throw new StandardException(
            StatusCodes.Status406NotAcceptable,
            "Standard.Exception",
            "Тестовый standard exception. Сообщение для разработчика.",
            "Тестовый standard exception. Сообщение для пользователя.",
            new NotImplementedException());
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
