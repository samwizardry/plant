using Asp.Versioning;
using OpenIddict.Abstractions;
using Plant.AspNetCore;

namespace AdaIdp.Controllers;

[ControllerName(ControllerName)]
public class IdpController : PlantController
{
    public const string ControllerName = "idp";
    private readonly IOpenIddictApplicationManager _applicationManager;

    public IdpController(IOpenIddictApplicationManager applicationManager)
    {
        _applicationManager = applicationManager;
    }



}
