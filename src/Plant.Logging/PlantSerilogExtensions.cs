using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Plant.Logging;
using Serilog;

namespace Microsoft.Extensions.DependencyInjection;

public static class PlantSerilogExtensions
{
    public static IHostBuilder UsePlantSerilog(this IHostBuilder builder)
    {
        return builder.UseSerilog((context, services, configuration) =>
        {
            configuration.ReadFrom.Configuration(context.Configuration);
        });
    }

    public static IApplicationBuilder UsePlantSerilogRequestLogging(this IApplicationBuilder builder)
    {
        return builder.UseSerilogRequestLogging(options =>
        {
            options.MessageTemplate = Constants.MessageTemplate;
            options.EnrichDiagnosticContext = LogHelper.EnrichFromRequest;
        });
    }
}
