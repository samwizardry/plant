using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Plant.Modules;

public interface IAsyncStartup
{
    /// <summary>
    /// This method gets called by the runtime. Use this method to configure the tenant pipeline.
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="routes"></param>
    /// <param name="serviceProvider"></param>
    ValueTask ConfigureAsync(IApplicationBuilder builder, IEndpointRouteBuilder routes, IServiceProvider serviceProvider);
}
