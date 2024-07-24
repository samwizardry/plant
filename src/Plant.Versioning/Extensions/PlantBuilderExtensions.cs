using Asp.Versioning;

namespace Microsoft.Extensions.DependencyInjection;

public static partial class PlantBuilderExtensions
{
    public static PlantVersioningBuilder AddVersioning(
        this PlantBuilder builder,
        Action<ApiVersioningOptions>? apiVersioningConfigure = null)
    {
        ArgumentNullException.ThrowIfNull(builder);

        var options = apiVersioningConfigure ?? (options =>
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
}
