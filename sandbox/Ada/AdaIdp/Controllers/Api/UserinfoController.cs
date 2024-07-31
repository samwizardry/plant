using Asp.Versioning;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;
using Plant.AspNetCore;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace AdaIdp.Controllers;

[ControllerName(ControllerName)]
public class UserinfoController : ApiControllerBase
{
    public const string ControllerName = "userinfo";

    [Authorize(AuthenticationSchemes = OpenIddictServerAspNetCoreDefaults.AuthenticationScheme)]
    [HttpGet, HttpPost]
    [Route("~/connect/userinfo")]
    public IActionResult Userinfo()
    {
        //var user = await _userManager.FindByIdAsync(User.GetClaim(Claims.Subject));
        //if (user is null)

        if (User.GetClaim(Claims.Subject) is null)
        {
            return Challenge(
                authenticationSchemes: OpenIddictServerAspNetCoreDefaults.AuthenticationScheme,
                properties: new AuthenticationProperties(new Dictionary<string, string?>
                {
                    [OpenIddictServerAspNetCoreConstants.Properties.Error] = Errors.InvalidToken,
                    [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] =
                        "The specified access token is bound to an account that no longer exists."
                }));
        }

        var claims = new Dictionary<string, object>(StringComparer.Ordinal)
        {
            // Note: the "sub" claim is a mandatory claim and must be included in the JSON response.
            [Claims.Subject] = User.GetClaim(Claims.Subject)!
        };

        if (User.HasScope(Scopes.Email) && User.GetClaim(Claims.Email) is string email)
        {
            claims[Claims.Email] = email;
            claims[Claims.EmailVerified] = true;
        }

        if (User.HasScope(Scopes.Phone) && User.GetClaim(Claims.PhoneNumber) is string phoneNumber)
        {
            claims[Claims.PhoneNumber] = phoneNumber;
            claims[Claims.PhoneNumberVerified] = true;
        }

        if (User.HasScope(Scopes.Roles))
        {
            claims[Claims.Role] = User.GetClaims(Claims.Role);
        }

        // Note: the complete list of standard claims supported by the OpenID Connect specification
        // can be found here: http://openid.net/specs/openid-connect-core-1_0.html#StandardClaims

        return Ok(claims);
    }
}
