using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Plant.Abstractions;

namespace Plant.Modules;

public abstract class StartupBase : IStartup, IAsyncStartup
{
    /// <inheritdoc />
    public virtual int Order { get; } = PlantConstants.ConfigureOrder.Default;

    /// <inheritdoc />
    public virtual int ConfigureOrder => Order;

    /// <inheritdoc />
    public virtual void ConfigureServices(IServiceCollection services)
    {
    }

    /// <inheritdoc />
    public virtual void Configure(IApplicationBuilder app, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
    {
    }

    /// <inheritdoc />
    public virtual ValueTask ConfigureAsync(IApplicationBuilder app, IEndpointRouteBuilder routes, IServiceProvider serviceProvider) => default;
}
