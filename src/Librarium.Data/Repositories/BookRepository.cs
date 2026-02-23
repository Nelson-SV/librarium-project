using Librarium.Data.Database;
using Librarium.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Librarium.Data.Repositories;

public class BookRepository(LibrariumDbContext dbContext)
{
    public async Task<List<Book>> GetBooks()
    {
        return await dbContext.Books.ToListAsync();
    }
}