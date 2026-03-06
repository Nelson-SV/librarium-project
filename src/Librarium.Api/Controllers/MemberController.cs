using Librarium.Api.Models.Dto.Request;
using Librarium.Data.Entities;
using Librarium.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Librarium.Api.Controllers;

[ApiController]
[Route("api/members")]
public class MemberController(MemberRepository memberRepository) : ControllerBase
{
    [HttpGet]
    [Route("all-members")]
    public async Task<IActionResult> GetMembersAsync()
    {
        var members = await memberRepository.GetMembersAsync();
    
        if (members.Count == 0)
        {
            return NotFound();
        }
    
        return Ok(members);
    }
    
    [HttpPut]
    [Route("update-member-details")]
    public async Task<IActionResult> UpdateMemberAsync(UpdateMemberDto dto)
    {
        var member = new Member
        {
            MemberId =  dto.MemberId,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,
        };
        
        var updated = await memberRepository.UpdateMemberAsync(member);
        
        if(updated.MemberId != dto.MemberId)
        {
            return NotFound();
        }
        
        return Ok(updated);
    }
}