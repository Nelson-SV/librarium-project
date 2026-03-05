using Librarium.Data.Database;
using Librarium.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Librarium.Data.Repositories;

public class LoanRepository(LibrariumDbContext dbContext)
{
    public async Task<List<Loan>> GetLoanFromMemberAsync(string memberId)
    {
        return await dbContext.Loans
            .Where(l => l.MemberId == memberId)
            .ToListAsync();
    }

    public async Task<Loan> CreateLoan(string memberId, string bookId)
    {
        var loan = new Loan()
        {
            LoanId = new Guid().ToString(),
            BookId = bookId,
            MemberId = memberId,
            LoanDate = DateTime.UtcNow,
            ReturnDate = DateTime.UtcNow.AddDays(7)
        };

        var result = await dbContext.Loans.AddAsync(loan);

        return result.Entity;
    }
}