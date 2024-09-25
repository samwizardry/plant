using Serilog;

namespace Microsoft.Extensions.Hosting;

public static partial class HostBuilderExtensions
{
    public static IHostBuilder UseSerilogHost(this IHostBuilder host)
    {
        ArgumentNullException.ThrowIfNull(host);

        return host.UseSerilog((context, services, configuration) =>
        {
            configuration.ReadFrom.Configuration(context.Configuration);
        });
    }
}
