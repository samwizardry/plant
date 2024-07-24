namespace Johna.Posts;

public class PostService
{
    private readonly List<PostModel> _posts = new List<PostModel>()
    {
        new PostModel
        {
            Id = 1,
            Title = "Hello, World!",
            Body = "This is new post by admin.",
            CreatedByName = "Admin",
            CreatedById = 1,
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow,
        }
    };

    public PostModel CreatePost(PostModel post)
    {
        _posts.Add(post);
        return post;
    }

    public IEnumerable<PostModel> GetPosts()
    {
        return _posts;
    }

    public PostModel? GetPostById(int postId)
    {
        return _posts.FirstOrDefault(e => e.Id == postId);
    }

    public void DeletePost(int postId)
    {
        if (GetPostById(postId) is PostModel post)
        {
            _posts.Remove(post);
        }
    }
}
