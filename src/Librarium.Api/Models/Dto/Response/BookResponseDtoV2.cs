namespace Librarium.Api.Models.Dto.Response;

public class BookResponseDtoV2
{
    public string BookId { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Isbn { get; set; }
    public int PublicationYear { get; set; }
}