namespace Librarium.Data.Entities;

public class BookAuthor
{
    public string Id { get; set; } = null!;
    public string AuthorId { get; set; } = null!;
    public string BookId { get; set; } = null!;
    
    // navigation properties (Foreign Keys)
    public Author Author { get; set; }
    public Book Book { get; set; }
}