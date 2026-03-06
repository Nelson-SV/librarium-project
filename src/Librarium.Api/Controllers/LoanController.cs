using Librarium.Api.Models.Dto.Response;
using Librarium.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Librarium.Api.Controllers;

[ApiController]
[Route("api/loans")]
public class LoanController(LoanRepository loanRepository) : ControllerBase
{
    [HttpGet]
    [Route("from-member/{memberId}")]
    public async Task<IActionResult> GetLoanFromMemberAsync(string memberId)
    {
        var loans = await loanRepository.GetLoanFromMemberAsync(memberId);
    
        var loanDtos = loans.Select(loan => new LoanResponseDto
        {
            LoanId = loan.LoanId,
            BookTitle = loan.Book.Title,
            LoanDate = loan.LoanDate,
            ReturnDate = loan.ReturnDate
        }).ToList();
        
        if (loanDtos.Count == 0)
        {
            return NotFound();
        }
    
        return Ok(loanDtos);
    }
    
    
    [HttpPost]
    [Route("create-loan")]
    public async Task<IActionResult> CreateLoan(string memberId, string bookId)
    {
        var result = await loanRepository.CreateLoanAsync(memberId, bookId);
    
        if (result.BookId != bookId || result.MemberId != memberId)
        {
            return BadRequest();
        }
    
        return Ok(result);
    }
    
}