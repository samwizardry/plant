namespace Johna.Posts;

public class PostModel
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Body { get; set; } = null!;

    public string CreatedByName { get; set; } = null!;

    public int CreatedById { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset UpdatedAt { get; set; }
}
