using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using OpenIddict.Validation.AspNetCore;
using Plant.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

#region Plant

builder.Host.UseSerilogHost();

builder.Services.AddPlant(plant =>
{
    plant.AddVersioning()
        .AddApiExplorer();

    plant.AddSwagger()
        .AddBearerSecurityDefinition()
        .AddVersionedDocs();

    plant.AddSecurity();
});

builder.Services.AddSingleton<StandardExceptionFilterAttribute>();
builder.Services.AddProblemDetails();
builder.Services.ConfigureOptions<ConfigureProblemDetailsOptions>();

#endregion

#region Authentication

builder.Services.AddAuthentication(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme);

#endregion

#region OpenIddict

// Register the OpenIddict validation components.
builder.Services.AddOpenIddict()
    .AddValidation(options =>
    {
        // Note: the validation handler uses OpenID Connect discovery
        // to retrieve the address of the introspection endpoint.
        options.SetIssuer("https://localhost:44319/");
        options.AddAudiences("ada_resource_server_a");

        // Configure the validation handler to use introspection and register the client
        // credentials used when communicating with the remote introspection endpoint.
        options.UseIntrospection()
               .SetClientId("ada_resource_server_a")
               .SetClientSecret("b777746b-06d9-4eb8-9831-f81e472507ed");

        // Register the System.Net.Http integration.
        options.UseSystemNetHttp();

        // Register the ASP.NET Core host.
        options.UseAspNetCore();
    });

#endregion

#region AdaResA Services

builder.Services.AddCors();

#endregion

var app = builder.Build();

app.UseCors(corsPolicy => corsPolicy
    .AllowAnyHeader()
    .AllowAnyMethod()
    .WithOrigins(
        "http://localhost:5260", "https://localhost:5261",
        "http://localhost:5270", "https://localhost:5271"));

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/api", [Authorize] (ClaimsPrincipal user) =>
{
    return $"{user.Identity!.Name} is allowed to access AdaResA resources.";
});

app.Run();
