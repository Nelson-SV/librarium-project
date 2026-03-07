namespace Librarium.Data.Entities;

public class Book
{
    public string BookId { get; set; } = null!;
    public string Title { get; set; } = null!;
    public long Isbn { get; set; }
    public int PublicationYear { get; set; }
    public bool IsDeleted { get; set; }

    // Navigation property
    public ICollection<BookAuthor> BookAuthors { get; set; } = [];
}