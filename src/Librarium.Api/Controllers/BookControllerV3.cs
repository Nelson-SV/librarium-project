using Librarium.Api.Models.Dto.Request;
using Librarium.Api.Models.Dto.Response;
using Librarium.Data.Entities;
using Librarium.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Librarium.Api.Controllers;


[ApiController]
[Route("api/v3/books")]
public class BookControllerV3(BookRepository bookRepository) : ControllerBase
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
    
    [HttpGet]
    [Route("books-with-authors")]
    public async Task<IActionResult> GetBooksWithAuthorsAsync()
    {
        var books = await bookRepository.GetBooksWithAuthorsAsync();
        
        if (books.Count == 0)
        {
            return NotFound();
        }
    
        var bookResponse = books.Select(b => new BookWithAuthorsResponseDtoV2
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
    
    [HttpPatch]
    [Route("update-book/{bookId}")]
    public async Task<IActionResult> UpdateBookAsync(UpdateBookRequestDto bookDto)
    {
        var book = new Book
        {
            BookId = bookDto.BookId,
            Title = bookDto.Title,
            PublicationYear = bookDto.PublicationYear,
            IsDeleted = bookDto.IsDeleted,
            Isbn = bookDto.Isbn
        };
        
        var updatedBook = await bookRepository.UpdateBookAsync(book);
        
        if(updatedBook.BookId != bookDto.BookId)
        {
            return NotFound();
        }
        
        var response = new UpdateBookResponseDto
        {
            BookId = updatedBook.BookId,
            Title = updatedBook.Title,
            PublicationYear = updatedBook.PublicationYear,
            IsDeleted = updatedBook.IsDeleted,
            Isbn = updatedBook.Isbn
        };
        
        return Ok(response);
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