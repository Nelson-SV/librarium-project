using Librarium.Api.Models.Dto.Request;
using Librarium.Api.Models.Dto.Response;
using Librarium.Data.Entities;
using Librarium.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Librarium.Api.Controllers;

[ApiController]
[Route("api/v2/loans")]
public class LoanControllerV2(LoanRepository loanRepository) : ControllerBase
{
    [HttpGet]
    [Route("from-member/{memberId}")]
    public async Task<IActionResult> GetLoanFromMemberAsync(string memberId)
    {
        var loans = await loanRepository.GetLoanFromMemberAsync(memberId);
    
        var loanDtos = loans.Select(loan => new LoanResponseDtoV2
        {
            LoanId = loan.LoanId,
            BookTitle = loan.Book.Title,
            LoanDate = loan.LoanDate,
            ReturnDate = loan.ReturnDate,
            Status =  loan.Status
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
    
    [HttpPut]
    [Route("update-loan")]
    public async Task<IActionResult> UpdateLoanAsync(UpdateLoanRequestDto requestDto)
    {
        var loan = new Loan
        {
            LoanId = requestDto.LoanId,
            MemberId = requestDto.MemberId,
            BookId = requestDto.BookId,
            LoanDate = requestDto.LoanDate,
            ReturnDate = requestDto.ReturnDate,
            Status = requestDto.Status
        };
        
        var updatedLoan = await loanRepository.UpdateLoanAsync(loan);
        
        if(updatedLoan.LoanId != requestDto.LoanId)
        {
            return NotFound();
        }

        var response = new UpdateLoanResponseDto
        {
            LoanId =  updatedLoan.LoanId,
            MemberId = updatedLoan.MemberId,
            BookId = updatedLoan.BookId,
            LoanDate = updatedLoan.LoanDate,
            ReturnDate = updatedLoan.ReturnDate,
            Status = updatedLoan.Status
        };
        
        return Ok(response);
    }
}