using Plant.Abstractions;

namespace Plant;

internal class ApplicationSettings : IApplication
{
    public string Title { get; set; } = null!;
}
