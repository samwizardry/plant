using Johna.Books;

namespace Johna.Books;

public class BookService
{
    private readonly List<Book> _books = new()
    {
        new Book { Id = 1, Title = "Housemaid's Secret", PublishedAt = new DateOnly(1998, 8, 15) },
        new Book { Id = 2, Title = "Saw", PublishedAt = new DateOnly(2008, 11, 27) },
        new Book { Id = 3, Title = "The Thing", PublishedAt = new DateOnly(2011, 3, 5) },
        new Book { Id = 4, Title = "The Journey Below", PublishedAt = new DateOnly(1987, 4, 9) }
    };

    public Book CreateBook(Book book)
    {
        _books.Add(book);
        return book;
    }

    public IEnumerable<Book> GetBooks()
    {
        return _books;
    }

    public Book? GetBookById(int bookId)
    {
        return _books.FirstOrDefault(e => e.Id == bookId);
    }

    public void DeleteBook(int bookId)
    {
        if (GetBookById(bookId) is Book book)
        {
            _books.Remove(book);
        }
    }
}
