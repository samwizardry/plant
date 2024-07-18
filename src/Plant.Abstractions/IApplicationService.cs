namespace Plant.Abstractions;

/// <summary>
/// Provides services to manage the application settings.
/// </summary>
public interface IApplicationService
{
    /// <summary>
    /// Loads the application settings from the store for updating and that should not be cached.
    /// </summary>
    Task<IApplication> LoadApplicationSettingsAsync();

    /// <summary>
    /// Gets the application settings from the cache for sharing and that should not be updated.
    /// </summary>
    Task<IApplication> GetApplicationSettingsAsync();

    /// <summary>
    /// Updates the store with the provided application settings and then updates the cache.
    /// </summary>
    Task UpdateApplicationSettingsAsync(IApplication application);
}
