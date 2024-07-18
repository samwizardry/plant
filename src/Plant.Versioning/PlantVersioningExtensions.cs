using Asp.Versioning;
using Plant.Abstractions;
using Plant.Versioning;

namespace Microsoft.Extensions.DependencyInjection;

public static class PlantVersioningExtensions
{
    public static PlantVersioningBuilder AddVersioning(
        this PlantBuilder builder,
        Action<ApiVersioningOptions>? setupAction = null)
    {
        ArgumentNullException.ThrowIfNull(builder);

        var options = setupAction ?? (options =>
        {
            // reporting api versions will return the headers
            // "api-supported-versions" and "api-deprecated-versions"
            options.ReportApiVersions = true;
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
        });

        var apiVersioningBuilder = builder.Services.AddApiVersioning(options);

        return new PlantVersioningBuilder(builder.Services, apiVersioningBuilder);
    }

    public static PlantBuilder AddVersioning(
        this PlantBuilder builder,
        Action<PlantVersioningBuilder> configure,
        Action<ApiVersioningOptions>? setupAction = null)
    {
        ArgumentNullException.ThrowIfNull(configure);

        var versioningBuilder = AddVersioning(builder: builder, setupAction: setupAction);
        configure(versioningBuilder);

        return builder;
    }
}
