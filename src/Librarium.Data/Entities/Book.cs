namespace Librarium.Data.Entities;

public class Book
{
    public string BookId { get; set; } = null!;
    public string Title { get; set; } = null!;
    public int PublicationYear { get; set; }
    public bool IsDeleted { get; set; }
    public string Isbn { get; set; } 


    // Navigation property
    public ICollection<BookAuthor> BookAuthors { get; set; } = [];
}