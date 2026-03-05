using Librarium.Data.Database;
using Librarium.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Librarium.Data.Repositories;

public class MemberRepository(LibrariumDbContext dbContext)
{
    public async Task<List<Member>> GetMembersAsync()
    {
        return await dbContext.Members.ToListAsync();
    }
}