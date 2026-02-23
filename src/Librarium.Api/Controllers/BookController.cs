using Librarium.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Librarium.Api.Controllers;

[ApiController]
[Route("api/books")]
public class BookController(BookRepository bookRepository) : ControllerBase
{
    [HttpGet]
    [Route("all-books")]
    public async Task<IActionResult> GetBooks()
    {
        var books = await bookRepository.GetBooks();
        
        if (books.Count == 0)
        {
            return NotFound();
        }
        
        return Ok(books);
    }
    
}