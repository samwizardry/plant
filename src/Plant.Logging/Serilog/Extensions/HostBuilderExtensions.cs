using Serilog;

namespace Microsoft.Extensions.Hosting;

public static partial class HostBuilderExtensions
{
    public static IHostBuilder UseSerilogHost(this IHostBuilder builder)
    {
        return builder.UseSerilog((context, services, configuration) =>
        {
            configuration.ReadFrom.Configuration(context.Configuration);
        });
    }
}
