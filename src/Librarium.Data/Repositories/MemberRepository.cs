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

    public async Task<Member> UpdateMemberAsync(Member member)
    {
        var updatedMember = dbContext.Members.Update(member).Entity;
        await dbContext.SaveChangesAsync();
        return updatedMember;
    }
}