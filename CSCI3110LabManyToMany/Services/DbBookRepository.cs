using CSCI3110LabManyToMany.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CSCI3110LabManyToMany.Services;

public class DbBookRepository : IBookRepository
{
    private readonly ApplicationDbContext _db;

    public DbBookRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<ICollection<Book>> ReadAllAsync()
    {
        return await _db.Books
            .Include(b => b.BookAuthors)
                .ThenInclude(ba => ba.Author)
            .ToListAsync();
    }

    public async Task<Book?> ReadAsync(int id)
    {
        return await _db.Books
            .Include(b => b.BookAuthors)
                .ThenInclude(ba => ba.Author)
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<bool> AssignAuthorToBookAsync(int bookId, int authorId)
    {
        var book = await ReadAsync(bookId);
        if (book == null) return false;

        // check if author already assigned
        if (book.BookAuthors != null && book.BookAuthors.Any(ba => ba.Author != null && ba.Author.Id == authorId))
        {
            return false;
        }

        var author = await _db.Authors
            .Include(a => a.BookAuthors)
            .FirstOrDefaultAsync(a => a.Id == authorId);

        if (author == null) return false;

        var bookAuthor = new BookAuthor
        {
            Book = book,
            Author = author
        };

        // ensure collections are initialized
        book.BookAuthors ??= new List<BookAuthor>();
        author.BookAuthors ??= new List<BookAuthor>();

        book.BookAuthors.Add(bookAuthor);
        author.BookAuthors.Add(bookAuthor);

        _db.BookAuthors.Add(bookAuthor);
        await _db.SaveChangesAsync();
        return true;
    }
}
