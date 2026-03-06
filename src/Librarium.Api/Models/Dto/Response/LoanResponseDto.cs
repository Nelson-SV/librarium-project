namespace Librarium.Api.Models.Dto.Response;

public class LoanResponseDto
{
    public string LoanId { get; set; }
    public string BookTitle { get; set; }
    public DateTime LoanDate { get; set; }
    public DateTime? ReturnDate { get; set; }
}