using Librarium.Api.Models.Dto.Request;
using Librarium.Api.Models.Dto.Response;
using Librarium.Data.Entities;
using Librarium.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Librarium.Api.Controllers;

[ApiController]
[Route("api/authors")]
public class AuthorController(AuthorRepository authorRepository) : ControllerBase
{
    [HttpPost]
    [Route("create-author")]
    public async Task<IActionResult> CreateAuthor(AuthorRequestDto author)
    {

        var authorToSave = new Author
        {
            AuthorId = Guid.NewGuid().ToString(),
            FirstName = author.FirstName,
            LastName = author.LastName,
            Biography = author.Biography
        };
            
        var result = await authorRepository.CreateAuthorAsync(authorToSave);
        
        if (result.AuthorId != authorToSave.AuthorId)
        {
            return BadRequest();
        }
        
        var authorResponse = new AuthorResponseDto
        {
            AuthorId = result.AuthorId,
            FirstName = result.FirstName,
            LastName = result.LastName,
            Biography = result.Biography
        };
    
        return Created($"/api/authors/{authorResponse.AuthorId}", authorResponse);
    }
}