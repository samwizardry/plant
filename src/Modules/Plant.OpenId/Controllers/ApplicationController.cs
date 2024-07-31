using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Abstractions;
using Plant.AspNetCore;

namespace Plant.OpenId.Controllers;

[ControllerName(ControllerName)]
[Route(ControllerRouteTemplate, Name = ControllerRouteName)]
public class ApplicationController : PlantController
{
    public const string ControllerName = "OpenIdApplication";
    public const string ControllerRouteName = "OpenIdApplication";
    public const string ControllerRouteTemplate = "/openid/application";

    private readonly IOpenIddictApplicationManager _applicationManager;

    public ApplicationController(IOpenIddictApplicationManager applicationManager)
    {
        _applicationManager = applicationManager;
    }

    public async Task<IActionResult> Index()
    {
        return View();
    }
}
