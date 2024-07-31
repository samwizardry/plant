using AdaIdp.Data;
using Microsoft.EntityFrameworkCore;
using OpenIddict.Abstractions;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace AdaIdp;

public class Worker : IHostedService
{
    private readonly IServiceProvider _provider;

    public Worker(IServiceProvider provider)
    {
        _provider = provider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await using var scope = _provider.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await context.Database.MigrateAsync(cancellationToken);

        var manager = scope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();

        if (await manager.FindByClientIdAsync("ada_client_x") is null)
        {
            await manager.CreateAsync(new OpenIddictApplicationDescriptor
            {
                ClientId = "ada_client_x",
                ClientSecret = "bb3bd8a0-4eb0-47ab-9511-5a9bc2476e9b",
                ApplicationType = ApplicationTypes.Web,
                ConsentType = ConsentTypes.Explicit,
                DisplayName = "Ada Client X",
                RedirectUris =
                {
                    new Uri("http://localhost:5260/sso/callback/login/local")
                },
                PostLogoutRedirectUris =
                {
                    new Uri("http://localhost:5260/sso/callback/logout/local")
                },
                Permissions =
                {
                    Permissions.Endpoints.Authorization,
                    Permissions.Endpoints.Logout,
                    Permissions.Endpoints.Token,
                    Permissions.GrantTypes.AuthorizationCode,
                    Permissions.GrantTypes.RefreshToken,
                    Permissions.ResponseTypes.Code,
                    Permissions.Scopes.Email,
                    Permissions.Scopes.Profile,
                    Permissions.Scopes.Roles
                },
                Requirements =
                {
                    Requirements.Features.ProofKeyForCodeExchange
                }
            });
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
