using Plant.Swagger;

namespace Microsoft.Extensions.DependencyInjection;

public static partial class PlantBuilderExtensions
{
    public static PlantSwaggerBuilder AddSwagger(this PlantBuilder plant)
    {
        ArgumentNullException.ThrowIfNull(plant);

        plant.Services.AddSwaggerGen(options =>
        {
            options.OperationFilter<SwaggerDefaultValues>();
        }).AddSwaggerGenNewtonsoftSupport();

        return new PlantSwaggerBuilder(plant.Services);
    }
}
