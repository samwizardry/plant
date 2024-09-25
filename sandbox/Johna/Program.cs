using Johna.Controllers;
using Plant.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

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

builder.Services
    .AddAuthentication()
    .AddCookie(options =>
    {
        string loginPath = AuthenticationController.ControllerRouteTemplate + "/" + AuthenticationController.LoginRouteTemplate;
        options.LoginPath = loginPath.StartsWith("~") ? new PathString(loginPath.Substring(1)) : new PathString(loginPath);

        string logoutPath = AuthenticationController.ControllerRouteTemplate + "/" + AuthenticationController.LogoutRouteTemplate;
        options.LogoutPath = logoutPath.StartsWith("~") ? new PathString(logoutPath.Substring(1)) : new PathString(logoutPath);

        options.ReturnUrlParameter = RazorConstants.ViewData.RedirectUri;
    });

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
