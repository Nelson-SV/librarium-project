namespace Librarium.Api.Models.Dto.Request;

public class UpdateBookRequestDto
{
    public string BookId { get; set; } = null!;
    public string Title { get; set; } = null!;
    public int PublicationYear { get; set; }
    public bool IsDeleted { get; set; }
    public string? Isbn { get; set; }
}