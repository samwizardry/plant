namespace Microsoft.Extensions.DependencyInjection;

public static class PlantExtensions
{
    public static PlantBuilder AddPlant(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        // Если объект PlantBuilder уже создан, используем его,
        // таким образом, можно вызвать AddPlant сколько угодно раз.
        var builder = services
            .LastOrDefault(d => d.ServiceType == typeof(PlantBuilder))?
            .ImplementationInstance as PlantBuilder;

        // Если объект PlantBuilder не найден, создаем новый.
        if (builder == null)
        {
            builder = new PlantBuilder(services);
            services.AddSingleton(builder);

            AddDefaultServices(builder);
        }

        return builder;
    }

    public static PlantBuilder AddPlant(this IServiceCollection services, Action<PlantBuilder> configure)
    {
        ArgumentNullException.ThrowIfNull(configure);

        var builder = services.AddPlant();

        configure(builder);

        return builder;
    }

    private static void AddDefaultServices(PlantBuilder builder)
    {
        builder.Services.AddHttpContextAccessor();
    }
}
