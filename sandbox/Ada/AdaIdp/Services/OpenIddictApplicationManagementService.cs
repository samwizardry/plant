using OpenIddict.Abstractions;

namespace AdaIdp.Services;

// TODO: Создать модели и добавить валидацию
// TODO: Создать стандартизированные исключения

public class OpenIddictApplicationManagementService(IOpenIddictApplicationManager applicationManager)
{
    private readonly IOpenIddictApplicationManager _applicationManager = applicationManager;

    // TODO: Перенести все в модель и добавить валидацию
    public async Task CreateApplicationAsync(
        string clientId,
        string? clientSecret = null,
        string? applicationType = null,
        string? clientType = null,
        string? consentType = null,
        string? displayName = null,
        IEnumerable<string>? redirectUris = null,
        IEnumerable<string>? postLogoutRedirectUris = null,
        IEnumerable<string>? permissions = null,
        IEnumerable<string>? requirements = null,
        CancellationToken cancellationToken = default)
    {
        var application = await _applicationManager.FindByClientIdAsync(clientId, cancellationToken);

        if (application is not null)
        {
            throw new InvalidOperationException($"Application with client id '{clientId}' already exists.");
        }

        application = new OpenIddictApplicationDescriptor()
        {
            ApplicationType = applicationType,
            ClientId = clientId,
            ClientSecret = clientSecret,
            ClientType = clientType,
            ConsentType = consentType,
            DisplayName = displayName
        };

        foreach (var uri in redirectUris?.Distinct() ?? Enumerable.Empty<string>())
        {
            ((OpenIddictApplicationDescriptor)application).RedirectUris.Add(new Uri(uri));
        }

        foreach (var uri in postLogoutRedirectUris?.Distinct() ?? Enumerable.Empty<string>())
        {
            ((OpenIddictApplicationDescriptor)application).PostLogoutRedirectUris.Add(new Uri(uri));
        }

        foreach (var permission in permissions?.Distinct() ?? Enumerable.Empty<string>())
        {
            ((OpenIddictApplicationDescriptor)application).Permissions.Add(permission);
        }

        foreach (var requirement in requirements?.Distinct() ?? Enumerable.Empty<string>())
        {
            ((OpenIddictApplicationDescriptor)application).Requirements.Add(requirement);
        }

        await _applicationManager.CreateAsync(application, cancellationToken);
    }

    public async Task<OpenIddictScopeDescriptor> FindScopeByNameAsync(string clientId, CancellationToken cancellationToken = default)
    {
        if (await _applicationManager.FindByClientIdAsync(clientId, cancellationToken) is OpenIddictScopeDescriptor scopeDescriptor)
        {
            return scopeDescriptor;
        }

        throw new NullReferenceException($"Application with '{clientId}' not found.");
    }
}
