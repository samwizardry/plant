using Microsoft.AspNetCore.Mvc;
using Plant;

namespace Johna.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[ServiceFilter(typeof(PlantExceptionFilterAttribute))]
public class ApiControllerBase : PlantControllerBase
{
}
