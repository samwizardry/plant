using System.Security.Claims;
using Asp.Versioning;
using Johna.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Plant.AspNetCore;

namespace Johna.Controllers;

public class User
{
    public Guid Id { get; set; } = Guid.Empty;

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public List<string> Roles { get; set; } = null!;
}

[ControllerName(ControllerName)]
[Route("~/auth", Name = ControllerName)]
public class AuthenticationController : PlantController
{
    public const string ControllerName = "Authentication";

    public const string LoginActionName = "Login";
    public const string LogoutActionName = "Logout";

    public const string LoginRouteName = $"{ControllerName}_{LoginActionName}";
    public const string LogoutRouteName = $"{ControllerName}_{LogoutActionName}";

    private readonly List<User> _users = new()
    {
        new User()
        {
            Id = new Guid("a2594817-8040-481f-a365-5ff9ffcf80d9"),
            UserName = "admin",
            Password = "P@ssw0rd",
            Email = "admin@gmail.com",
            Roles = ["Admin"]
        },
        new User()
        {
            Id = new Guid("ee3e3fc9-2ad5-4625-be62-49247473f2fd"),
            UserName = "Johna",
            Password = "Johna123",
            Email = "johna@gmail.com",
            Roles = ["Manager", "Moderator"]
        },
    };

    [ActionName(LoginActionName)]
    [HttpGet("login", Name = LoginRouteName)]
    public IActionResult Login([FromQuery] string? redirectUri)
    {
        if (User.Identity is { IsAuthenticated: true })
        {
            return Url.IsLocalUrl(redirectUri) ? LocalRedirect(redirectUri) : LocalRedirect("~/");
        }

        ViewData[RazorConstants.ViewData.RedirectUri] = redirectUri;
        return View(new SignInViewModel());
    }

    [ActionName(LoginActionName)]
    [HttpPost("login", Name = LoginRouteName)]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login([FromForm] SignInViewModel model, [FromQuery] string? redirectUri)
    {
        var user = _users.FirstOrDefault(e => e.Email.Equals(model.Email, StringComparison.OrdinalIgnoreCase) && e.Password.Equals(model.Password));

        if (user is not null)
        {
            ClaimsIdentity identity = CreateIdentity(user, CookieAuthenticationDefaults.AuthenticationScheme);
            ClaimsPrincipal principal = new(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }
        else
        {
            ViewData[RazorConstants.ViewData.RedirectUri] = redirectUri;
            return View(model);
        }

        return Url.IsLocalUrl(redirectUri) ? LocalRedirect(redirectUri) : LocalRedirect("~/");
    }

    [ActionName(LogoutActionName)]
    [HttpPost("logout", Name = LogoutRouteName)]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        if (result.Succeeded)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        return LocalRedirect("~/");
    }

    private ClaimsIdentity CreateIdentity(User user, string? authenticationScheme)
    {
        List<Claim> claims = new()
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.UserName)
        };

        foreach (var role in user.Roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var identity = new ClaimsIdentity(
            claims,
            authenticationType: authenticationScheme,
            nameType: ClaimTypes.Name,
            roleType: ClaimTypes.Role);

        return identity;
    }
}
