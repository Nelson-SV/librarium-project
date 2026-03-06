namespace Librarium.Api.Models.Dto.Response;

public class UpdateLoanResponseDto
{
    public string LoanId { get; set; } = null!;
    public string MemberId { get; set; }
    public string BookId { get; set; }
    public DateTime LoanDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public string? Status { get; set; }
}