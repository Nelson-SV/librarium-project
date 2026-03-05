using Librarium.Data.Database;
using Librarium.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Librarium.Data.Repositories;

public class BookRepository(LibrariumDbContext dbContext)
{
    public async Task<List<Book>> GetBooksAsync()
    {
        return await dbContext.Books.ToListAsync();
    }

    public async Task<List<Book>> GetBooksWithAuthorsAsync()
    {
        return await dbContext.Books
            .Include(b => b.BookAuthors)
            .ThenInclude(ba => ba.Author)
            .ToListAsync();
    }
}