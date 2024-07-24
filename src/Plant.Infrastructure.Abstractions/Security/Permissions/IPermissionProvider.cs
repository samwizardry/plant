namespace Plant.Security.Permissions;

/// <summary>
/// Implemented by modules to enumerate the types of permissions
/// the which may be granted.
/// </summary>
public interface IPermissionProvider
{
    Task<IEnumerable<Permission>> GetPermissionsAsync();

    IEnumerable<PermissionStereotype> GetDefaultStereotypes();
}

public class PermissionStereotype
{
    public string Name { get; set; } = null!;

    public IEnumerable<Permission> Permissions { get; set; } = null!;
}
