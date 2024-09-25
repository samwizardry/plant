using Asp.Versioning;

namespace Microsoft.Extensions.DependencyInjection;

public static partial class PlantBuilderExtensions
{
    public static PlantVersioningBuilder AddVersioning(
        this PlantBuilder plant,
        Action<ApiVersioningOptions>? setupAction = null)
    {
        ArgumentNullException.ThrowIfNull(plant);

        var apiVersioningBuilder = plant.Services.AddApiVersioning(setupAction ?? (options =>
        {
            // reporting api versions will return the headers
            // "api-supported-versions" and "api-deprecated-versions"
            options.ReportApiVersions = true;
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
        }));

        return new PlantVersioningBuilder(plant.Services, apiVersioningBuilder);
    }
}
