namespace Librarium.Api.Models.Dto.Response;

public class AuthorResponseDto
{
    public string AuthorId { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? Biography { get; set; }
}