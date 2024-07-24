using Microsoft.AspNetCore.Authorization;
using Plant.Security;
using Plant.Security.AuthorizationHandlers;

namespace Microsoft.Extensions.DependencyInjection;

public static partial class PlantBuilderExtensions
{
    public static PlantBuilder AddSecurity(this PlantBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Services.AddAuthorization();

        builder.Services.AddSingleton<IPermissionGrantingService, DefaultPermissionGrantingService>();
        builder.Services.AddSingleton<IAuthorizationHandler, PermissionHandler>();

        return builder;
    }
}
