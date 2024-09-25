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
        options.LoginPath = "/auth/login";
        options.LogoutPath = "/auth/logout";
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

app.UsePlantSerilogRequestLogging();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapDefaultControllerRoute();

app.Run();
