namespace Johna.Books;

public class Book
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public DateOnly PublishedAt { get; set; }
}
