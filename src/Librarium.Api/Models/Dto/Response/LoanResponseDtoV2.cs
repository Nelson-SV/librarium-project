namespace Librarium.Api.Models.Dto.Response;

public class LoanResponseDtoV2
{
    public string LoanId { get; set; }
    public string BookTitle { get; set; }
    public DateTime LoanDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public string? Status { get; set; }
}