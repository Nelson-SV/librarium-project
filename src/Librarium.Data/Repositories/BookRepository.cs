using Librarium.Data.Database;
using Librarium.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Librarium.Data.Repositories;

public class BookRepository(LibrariumDbContext dbContext)
{
    public async Task<List<Book>> GetBooksAsync()
    {
        return await dbContext.Books
            .Where(b => !b.IsDeleted)
            .ToListAsync();
    }

    public async Task<List<Book>> GetBooksWithAuthorsAsync()
    {
        return await dbContext.Books
            .Where(b => !b.IsDeleted)
            .Include(b => b.BookAuthors)
            .ThenInclude(ba => ba.Author)
            .ToListAsync();
    }
    
    public async Task<bool> DeleteBookAsync(string bookId)
    {
        var book = await dbContext.Books.FindAsync(bookId);
        if (book == null) return false; 

        book.IsDeleted = true;
        dbContext.Entry(book).State = EntityState.Modified;

        await dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<Book> UpdateBookAsync(Book book)
    {
        var updatedBook = dbContext.Books.Update(book).Entity;
        await dbContext.SaveChangesAsync();
        return updatedBook;
    }
}