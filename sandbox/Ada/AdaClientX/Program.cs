using AdaClientX;
using AdaClientX.Controllers;
using AdaClientX.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using OpenIddict.Client;
using Plant.AspNetCore;
using static OpenIddict.Abstractions.OpenIddictConstants;

var builder = WebApplication.CreateBuilder(args);

#region Plant

builder.Host.UseSerilogHost();

builder.Services.AddPlant(plant =>
{
    plant.AddVersioning()
        .AddMvc()
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

#region authentication

builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = new PathString($"{AuthenticationController.SsoLoginRouteTemplate}/{AuthenticationController.SsoLoginLocalProvider}");
        options.LogoutPath = new PathString($"{AuthenticationController.SsoLogoutRouteTemplate}/{AuthenticationController.SsoLoginLocalProvider}");
        options.ReturnUrlParameter = RazorConstants.ViewData.RedirectUri;
        options.ExpireTimeSpan = TimeSpan.FromHours(1.0d);
        options.SlidingExpiration = false;
    });

#endregion

#region OpenIddict

builder.Services.AddOpenIddict()
    // Register the OpenIddict core components.
    .AddCore(options =>
    {
        options.UseEntityFrameworkCore()
            .UseDbContext<ApplicationDbContext>();
    })
    .AddClient(options =>
    {
        // Note: this sample uses the authorization code flow,
        // but you can enable the other flows if necessary.
        options.AllowAuthorizationCodeFlow()
            .AllowRefreshTokenFlow();

        // Register the signing and encryption credentials used to protect
        // sensitive data like the state tokens produced by OpenIddict.
        options.AddDevelopmentEncryptionCertificate()
            .AddDevelopmentSigningCertificate();

        // Register the ASP.NET Core host and configure the ASP.NET Core-specific options.
        options.UseAspNetCore()
            .EnableStatusCodePagesIntegration()
            .EnableRedirectionEndpointPassthrough()
            .EnablePostLogoutRedirectionEndpointPassthrough()
            .DisableTransportSecurityRequirement();

        // Add the operating system integration.
        options.UseSystemIntegration();

        // Register the System.Net.Http integration and use the identity of the current
        // assembly as a more specific user agent, which can be useful when dealing with
        // providers that use the user agent as a way to throttle requests (e.g Reddit).
        options.UseSystemNetHttp()
            .SetProductInformation(typeof(Program).Assembly);

        options.AddRegistration(new OpenIddictClientRegistration
        {
            Issuer = new Uri("http://localhost:5250", UriKind.Absolute),
            ClientId = "ada_client_x",
            ClientSecret = "bb3bd8a0-4eb0-47ab-9511-5a9bc2476e9b",
            Scopes =
            {
                Scopes.Email,
                Scopes.Profile,
                Scopes.Roles
            },

            RedirectUri = new Uri($"{AuthenticationController.SsoLoginCallbackRouteTemplate}/{AuthenticationController.SsoLoginLocalProvider}", UriKind.Relative),
            PostLogoutRedirectUri = new Uri($"{AuthenticationController.SsoLogoutCallbackRouteTemplate}/{AuthenticationController.SsoLoginLocalProvider}", UriKind.Relative)
        });
    });

#endregion

#region Data

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("Sqlite"));

    options.UseOpenIddict();
});

#endregion

#region AdaIdp services

builder.Services.AddControllersWithViews();
builder.Services.AddHostedService<Worker>();

#endregion

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger();
app.UseVersionedSwaggerUI();

app.UseStaticFiles();

app.UseSerilogRequestLogging();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapDefaultControllerRoute();

app.Run();
