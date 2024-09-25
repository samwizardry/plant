using Plant.Abstractions;
using Plant.Serilog;
using Serilog;

namespace Microsoft.AspNetCore.Builder;

public static partial class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseSerilogRequestLogging(this IApplicationBuilder app)
    {
        ArgumentNullException.ThrowIfNull(app);

        app.UseSerilogRequestLogging(options =>
        {
            options.MessageTemplate = PlantConstants.Serilog.MessageTemplate;
            options.EnrichDiagnosticContext = LogHelper.EnrichFromRequest;
        });

        return app;
    }
}
