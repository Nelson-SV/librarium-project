namespace Librarium.Api.Models.Dto.Response;

public class BookResponseDto
{
    public string BookId { get; set; } = null!;
    public string Title { get; set; } = null!;
    public long Isbn { get; set; }
    public int PublicationYear { get; set; }
}