using Librarium.Api.Models.Dto;
using Librarium.Api.Models.Dto.Response;
using Librarium.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Librarium.Api.Controllers;


[ApiController]
[Route("api/v2/books")]
public class BookControllerV2(BookRepository bookRepository) : ControllerBase
{
    [HttpGet]
    [Route("all-books")]
    public async Task<IActionResult> GetBooksWithAuthorsAsync()
    {
        var books = await bookRepository.GetBooksWithAuthorsAsync();
        
        if (books.Count == 0)
        {
            return NotFound();
        }

        var bookResponse = books.Select(b => new BookWithAuthorsResponseDto
        {
            BookId = b.BookId,
            Title = b.Title,
            Isbn = b.Isbn,
            PublicationYear = b.PublicationYear,
            Authors = b.BookAuthors.Select(ba => new AuthorResponseDto
            {
                AuthorId = ba.AuthorId,
                FirstName = ba.Author.FirstName,
                LastName = ba.Author.LastName,
                Biography = ba.Author.Biography,
            }).ToList()

        });
        
        return Ok(bookResponse);
    }
}