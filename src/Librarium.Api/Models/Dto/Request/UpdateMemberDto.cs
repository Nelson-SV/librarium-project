namespace Librarium.Api.Models.Dto.Request;

public class UpdateMemberDto
{
    public string MemberId { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    // public string? PhoneNumber { get; set; }
}