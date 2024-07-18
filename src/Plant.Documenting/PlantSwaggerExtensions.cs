using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.Builder;
using Plant.Abstractions;
using Plant.Documenting;

namespace Microsoft.Extensions.DependencyInjection;

public static class PlantSwaggerExtensions
{
    public static PlantBuilder AddPlantSwagger(this PlantBuilder builder)
    {
        builder.Services.AddSwaggerGen(options =>
        {
            options.OperationFilter<SwaggerDefaultValues>();
        }).AddSwaggerGenNewtonsoftSupport();

        builder.Services.ConfigureOptions<ConfigureSwaggerGenOptions>();

        return builder;
    }

    public static IApplicationBuilder UsePlantSwagger(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            var apiVersionDescriptionProvider = app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();

            foreach (var apiVersionDescription in apiVersionDescriptionProvider.ApiVersionDescriptions)
            {
                options.SwaggerEndpoint($"/swagger/{apiVersionDescription.GroupName}/swagger.json",
                apiVersionDescription.GroupName.ToUpperInvariant());
            }

            options.RoutePrefix = "swagger";
        });

        return app;
    }
}
