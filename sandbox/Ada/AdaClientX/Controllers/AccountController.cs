using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Plant.AspNetCore;

namespace AdaClientX.Controllers;

[Authorize]
[Route(ControllerRouteTemplate, Name = ControllerRouteName)]
[ControllerName(ControllerName)]
public class AccountController : PlantController
{
    public const string ControllerName = "account";
    public const string ControllerRouteTemplate = "~/account";
    public const string ControllerRouteName = "account";

    public AccountController() { }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
}
