using Plant.Swagger;

namespace Microsoft.Extensions.DependencyInjection;

public static partial class PlantBuilderExtensions
{
    public static PlantSwaggerBuilder AddSwagger(this PlantBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Services.AddSwaggerGen(options =>
        {
            options.OperationFilter<SwaggerDefaultValues>();
        }).AddSwaggerGenNewtonsoftSupport();

        return new PlantSwaggerBuilder(builder.Services);
    }
}
