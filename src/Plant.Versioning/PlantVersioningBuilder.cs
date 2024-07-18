using System.ComponentModel;
using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;

namespace Plant.Versioning;

public sealed class PlantVersioningBuilder
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public IServiceCollection Services { get; }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public IApiVersioningBuilder ApiVersioningBuilder { get; }

    public PlantVersioningBuilder(IServiceCollection services, IApiVersioningBuilder apiVersioningBuilder)
    {
        Services = services ?? throw new ArgumentNullException(nameof(services));
        ApiVersioningBuilder = apiVersioningBuilder ?? throw new ArgumentNullException(nameof(apiVersioningBuilder));
    }

    public PlantVersioningBuilder AddMvc()
    {
        ApiVersioningBuilder.AddMvc();
        return this;
    }

    public PlantVersioningBuilder AddMvc(Action<MvcApiVersioningOptions> setupAction)
    {
        ApiVersioningBuilder.AddMvc(setupAction);
        return this;
    }

    public PlantVersioningBuilder AddApiExplorer()
    {
        ApiVersioningBuilder.AddApiExplorer(options =>
        {
            // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
            // note: the specified format code will format the version as "'v'major[.minor][-status]"
            options.GroupNameFormat = "'v'VVV";

            // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
            // can also be used to control the format of the API version in route templates
            options.SubstituteApiVersionInUrl = true;
        });

        return this;
    }

    public PlantVersioningBuilder AddApiExplorer(Action<ApiExplorerOptions> setupAction)
    {
        ApiVersioningBuilder.AddApiExplorer(setupAction);
        return this;
    }
}
