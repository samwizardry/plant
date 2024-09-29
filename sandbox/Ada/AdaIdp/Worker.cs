using AdaIdp.Data;
using AdaIdp.Services;
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

        var openIddictApplicationManagementService = scope.ServiceProvider.GetRequiredService<OpenIddictApplicationManagementService>();
        var openIddictScopeManagementService = scope.ServiceProvider.GetRequiredService<OpenIddictScopeManagementService>();

        // TODO: создать апп и скопы для ресурсных серверов
        // TODO: -shm и -wal, что это?
        // TODO: .db добавить в gitignore

        try
        {
            await openIddictApplicationManagementService.CreateApplicationAsync(
                applicationType: ApplicationTypes.Web,
                clientId: "ada_client_x",
                clientSecret: "bb3bd8a0-4eb0-47ab-9511-5a9bc2476e9b",
                consentType: ConsentTypes.Explicit,
                displayName: "Ada Client X",
                redirectUris: ["http://localhost:5260/sso/callback/login/local"],
                postLogoutRedirectUris: ["http://localhost:5260/sso/callback/logout/local"],
                permissions: [Permissions.Endpoints.Authorization,
                    Permissions.Endpoints.Logout,
                    Permissions.Endpoints.Token,
                    Permissions.GrantTypes.AuthorizationCode,
                    Permissions.GrantTypes.RefreshToken,
                    Permissions.ResponseTypes.Code,
                    Permissions.Scopes.Email,
                    Permissions.Scopes.Profile,
                    Permissions.Scopes.Roles],
                requirements: [Requirements.Features.ProofKeyForCodeExchange]);
        }
        catch { }

        try
        {
            await openIddictScopeManagementService.CreateScopeAsync("AdaResA", ["ada_resource_server_a"]);
        }
        catch { }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
