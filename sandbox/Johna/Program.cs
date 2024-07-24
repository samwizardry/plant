using Plant.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

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
builder.Services.AddProblemDetails(ProblemDetailsHelper.ConfigureProblemDetailsOptions);

builder.Services.AddAuthentication();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger();
app.UseVersionedSwaggerUI();
app.UsePlantSerilogRequestLogging();

app.UseAuthentication();
app.UseAuthorization();

app.UseRouting();
app.MapControllers();

app.Run();
