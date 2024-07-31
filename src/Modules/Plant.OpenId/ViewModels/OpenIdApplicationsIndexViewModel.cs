namespace Plant.OpenId.ViewModels;

public class OpenIdApplicationsIndexViewModel
{
    public IList<OpenIdApplicationEntry> Applications { get; } = [];
}

public class OpenIdApplicationEntry
{
    public string Id { get; set; }

    public string DisplayName { get; set; }

    public bool IsChecked { get; set; }
}
