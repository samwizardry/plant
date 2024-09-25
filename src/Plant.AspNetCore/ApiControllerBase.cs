using Microsoft.AspNetCore.Mvc;

namespace Plant.AspNetCore;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[ServiceFilter(typeof(StandardExceptionFilterAttribute))]
public abstract class ApiControllerBase : PlantControllerBase;
