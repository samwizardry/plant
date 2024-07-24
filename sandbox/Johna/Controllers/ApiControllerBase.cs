using Microsoft.AspNetCore.Mvc;
using Plant.AspNetCore;

namespace Johna.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[ServiceFilter(typeof(StandardExceptionFilterAttribute))]
public class ApiControllerBase : PlantControllerBase
{
}
