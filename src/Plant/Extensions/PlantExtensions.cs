using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Options;
using Plant;
using Plant.Abstractions;

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

            // TODO: Небоходимые для библиотеки сервисы
            //services.AddHttpContextAccessor();
            //services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddSingleton<PlantExceptionFilterAttribute>();
            services.AddSingleton<ProblemDetailsFactory, PlantProblemDetailsFactory>();
            services.AddSingleton<IApplicationService, ApplicationService>();
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
}
