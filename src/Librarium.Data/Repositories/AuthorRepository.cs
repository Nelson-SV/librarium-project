using Librarium.Data.Database;
using Librarium.Data.Entities;

namespace Librarium.Data.Repositories;

public class AuthorRepository(LibrariumDbContext dbContext)
{
    public async Task<Author> CreateAuthorAsync(Author author)
    {
        var result = await dbContext.Author.AddAsync(author);
        return result.Entity;
    }
}