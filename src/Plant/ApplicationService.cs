using Plant.Abstractions;

namespace Plant;

/// <inheritdoc />
internal class ApplicationService : IApplicationService
{
    /// <inheritdoc />
    public async Task<IApplication> GetApplicationSettingsAsync()
    {
        return await GetDefaultSettingsAsync();
    }

    /// <inheritdoc />
    public async Task<IApplication> LoadApplicationSettingsAsync()
    {
        return await GetDefaultSettingsAsync();
    }

    /// <inheritdoc />
    public Task UpdateApplicationSettingsAsync(IApplication application)
    {
        throw new NotImplementedException();
    }

    private Task<ApplicationSettings> GetDefaultSettingsAsync()
    {
        return Task.FromResult(new ApplicationSettings
        {
            Title = "Plant Application"
        });
    }
}
