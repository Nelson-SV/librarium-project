namespace Librarium.Data.Entities;

public class Loan
{
    public string LoanId { get; set; } = null!;
    public string MemberId { get; set; } = null!;
    public string BookId { get; set; } = null!;
    public DateTime LoanDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    
    // navigation properties (Foreign Keys)
    public Member Member { get; set; }
    public Book Book { get; set; }
}