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
            .Include(l => l.Book)
            .ToListAsync();
    }

    public async Task<Loan> CreateLoanAsync(string memberId, string bookId)
    {
        var book = await dbContext.Books.FindAsync(bookId);
        
        if (book == null)
            throw new KeyNotFoundException("Book not found.");

        if (book.IsDeleted)
            throw new InvalidOperationException("Book is retired and cannot be loaned out.");
        
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

    public async Task<Loan> UpdateLoanAsync(Loan loan)
    {
        var updatedLoan = dbContext.Loans.Update(loan).Entity;
        await dbContext.SaveChangesAsync();
        return updatedLoan;
    }
}