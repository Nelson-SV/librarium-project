namespace Librarium.Api.Models.Dto.Response;

public class UpdateBookResponseDto
{
    public string BookId { get; set; } = null!;
    public string Title { get; set; } = null!;
    public int PublicationYear { get; set; }
    public bool IsDeleted { get; set; }
    public string? Isbn { get; set; }
}