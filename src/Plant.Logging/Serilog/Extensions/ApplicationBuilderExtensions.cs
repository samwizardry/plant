using Plant.Serilog;
using Serilog;

namespace Microsoft.AspNetCore.Builder;

public static partial class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UsePlantSerilogRequestLogging(this IApplicationBuilder app)
    {
        ArgumentNullException.ThrowIfNull(app);

        app.UseSerilogRequestLogging(options =>
        {
            options.MessageTemplate = "{Protocol} {RequestMethod} {RequestPath}{QueryString} responded {StatusCode} in {Elapsed:0.0000} ms";
            options.EnrichDiagnosticContext = LogHelper.EnrichFromRequest;
        });

        return app;
    }
}
