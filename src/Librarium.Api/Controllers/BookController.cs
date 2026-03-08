using Librarium.Api.Models.Dto.Response;
using Librarium.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Librarium.Api.Controllers;

[ApiController]
[Route("api/books")]
public class BookController(BookRepository bookRepository) : ControllerBase
{
    [HttpGet]
    [Route("all-books")]
    public async Task<IActionResult> GetBooksAsync()
    {
        var books = await bookRepository.GetBooksAsync();
        
        if (books.Count == 0)
        {
            return NotFound();
        }
        
        var booksResponse = books.Select(b => new BookResponseDtoV2
        {
            BookId = b.BookId,
            Title = b.Title,
            Isbn = b.Isbn,
            PublicationYear = b.PublicationYear,
        });
        
        return Ok(booksResponse);
    }
    
    [HttpPatch]
    [Route("delete-book/{bookId}")]
    public async Task<IActionResult> DeleteBookAsync(string bookId)
    {
        var isDeleted = await bookRepository.DeleteBookAsync(bookId);

        if (!isDeleted)
        {
            return NotFound();
        }

        return Ok(isDeleted);
    }
    
}