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
    public async Task<IActionResult> CreateLoanAsync(string memberId, string bookId)
    {
        try
        {
            var loan = await loanRepository.CreateLoanAsync(memberId, bookId);
            return Ok(loan);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
}