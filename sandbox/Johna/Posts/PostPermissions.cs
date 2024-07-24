using Plant.Security.Permissions;

namespace Johna.Posts;

public class PostPermissions : IPermissionProvider
{
    public static readonly Permission ManagePosts = new(nameof(ManagePosts), "Управление публикациями") { Category = "Публикации" };
    public static readonly Permission CreatePosts = new(nameof(CreatePosts), "Создание публикаций", [ManagePosts]) { Category = "Публикации" };
    public static readonly Permission DeletePosts = new(nameof(DeletePosts), "Удаление публикаций", [ManagePosts]) { Category = "Публикации" };
    public static readonly Permission EditPosts = new(nameof(EditPosts), "Изменение публикаций", [CreatePosts]) { Category = "Публикации" };
    public static readonly Permission ListPosts = new(nameof(ListPosts), "Просмотр списка публикаций", [CreatePosts, EditPosts, DeletePosts]) { Category = "Публикации" };

    public Task<IEnumerable<Permission>> GetPermissionsAsync()
    {
        return Task.FromResult(new Permission[]
        {
            ManagePosts,
            CreatePosts,
            DeletePosts,
            EditPosts,
            ListPosts
        }.AsEnumerable());
    }

    public IEnumerable<PermissionStereotype> GetDefaultStereotypes()
    {
        throw new NotImplementedException();
    }
}
