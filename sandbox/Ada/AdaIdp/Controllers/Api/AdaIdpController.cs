using AdaIdp.Data;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Plant.AspNetCore;

namespace AdaIdp.Controllers;

[ControllerName(ControllerName)]
public class AdaIdpController : ApiControllerBase
{
    public const string ControllerName = "ada";

    [HttpGet]
    [Route("characters")]
    public async Task<IActionResult> ListCharacters([FromServices] ApplicationDbContext context, CancellationToken cancellationToken)
    {
        var characters = await context.Characters.ToListAsync(cancellationToken);
        return Ok(characters);
    }
}
