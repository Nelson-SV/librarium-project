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
}