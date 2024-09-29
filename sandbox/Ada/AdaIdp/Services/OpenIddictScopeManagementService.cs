using System.Threading;
using OpenIddict.Abstractions;

namespace AdaIdp.Services;

public class OpenIddictScopeManagementService(IOpenIddictScopeManager scopeManager)
{
    private readonly IOpenIddictScopeManager _scopeManager = scopeManager;

    // TODO: Создать модель и добавить валидацию
    public async Task CreateScopeAsync(
        string name,
        IEnumerable<string> resources,
        CancellationToken cancellationToken = default)
    {
        var scope = await _scopeManager.FindByNameAsync(name, cancellationToken);

        if (scope is not null)
        {
            throw new InvalidOperationException($"Scope '{name}' already exists.");
        }

        scope = new OpenIddictScopeDescriptor()
        {
            Name = name
        };

        foreach (var resource in resources)
        {
            ((OpenIddictScopeDescriptor)scope).Resources.Add(resource);
        }

        await _scopeManager.CreateAsync(scope, cancellationToken);
    }

    // TODO: Создать модель и добавить валидацию
    public async Task<OpenIddictScopeDescriptor> FindScopeByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        if (await _scopeManager.FindByNameAsync(name, cancellationToken) is OpenIddictScopeDescriptor scopeDescriptor)
        {
            return scopeDescriptor;
        }

        throw new NullReferenceException($"Scope '{name}' not found.");
    }
}
