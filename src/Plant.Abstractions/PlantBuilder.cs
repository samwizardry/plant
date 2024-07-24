namespace Microsoft.Extensions.DependencyInjection;

public sealed class PlantBuilder
{
    public IServiceCollection Services { get; }

    public PlantBuilder(IServiceCollection services)
        => Services = services ?? throw new ArgumentNullException(nameof(services));
}
