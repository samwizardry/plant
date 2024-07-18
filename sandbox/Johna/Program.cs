var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Host.UsePlantSerilog();

builder.Services.AddPlant(plant =>
{
    plant.AddVersioning()
        .AddMvc()
        .AddApiExplorer();

    plant.AddPlantSwagger();
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UsePlantSwagger();
app.UsePlantSerilogRequestLogging();

app.UseRouting();
app.MapControllers();

app.Run();
