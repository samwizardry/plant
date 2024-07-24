using Plant.Swagger;

namespace Microsoft.Extensions.DependencyInjection;

public sealed class PlantSwaggerBuilder
{
    public IServiceCollection Services { get; }

    public PlantSwaggerBuilder(IServiceCollection services)
        => Services = services ?? throw new ArgumentNullException(nameof(services));

    public PlantSwaggerBuilder AddBearerSecurityDefinition()
    {
        Services.ConfigureOptions<ConfigureSwaggerBearerSecurityOptions>();
        return this;
    }

    public PlantSwaggerBuilder AddVersionedDocs()
    {
        Services.ConfigureOptions<ConfigureSwaggerVersionedDocsOptions>();
        return this;
    }
}
