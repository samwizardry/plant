using Plant.Security.Permissions;

namespace Plant.Security;

public static class StandardPermissions
{
    public static readonly Permission SuperUser = new(nameof(SuperUser), "Super Users Permission", isSecurityCritical: true);
}
