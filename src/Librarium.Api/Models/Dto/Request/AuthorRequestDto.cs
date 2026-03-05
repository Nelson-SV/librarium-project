namespace Librarium.Api.Models.Dto.Request;

public class AuthorRequestDto
{
    public string AuthorId { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? Biography { get; set; }
}