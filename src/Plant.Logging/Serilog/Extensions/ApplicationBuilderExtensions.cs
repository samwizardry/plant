using Plant.Abstractions;
using Plant.Serilog;
using Serilog;

namespace Microsoft.AspNetCore.Builder;

public static partial class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UsePlantSerilogRequestLogging(this IApplicationBuilder builder)
    {
        return builder.UseSerilogRequestLogging(options =>
        {
            options.MessageTemplate = PlantConstants.Serilog.MessageTemplate;
            options.EnrichDiagnosticContext = LogHelper.EnrichFromRequest;
        });
    }
}
