using Microsoft.AspNetCore.Authorization;
using Plant.Security.Permissions;

namespace Plant.Security;

public class PermissionRequirement : IAuthorizationRequirement
{
    public Permission Permission { get; set; }

    public PermissionRequirement(Permission permission)
    {
        ArgumentNullException.ThrowIfNull(permission);

        Permission = permission;
    }
}
