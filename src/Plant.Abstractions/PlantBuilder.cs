using System.ComponentModel;
using Microsoft.Extensions.DependencyInjection;

namespace Plant.Abstractions;

public sealed class PlantBuilder
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public IServiceCollection Services { get; }

    public PlantBuilder(IServiceCollection services)
        => Services = services ?? throw new ArgumentNullException(nameof(services));
}
