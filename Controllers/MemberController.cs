using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TournamentManager.Backend.Models;
using TournamentManager.Backend.Services;

namespace TournamentManager.Backend.Controllers
{
    [Produces("application/json")]
    [Route("api/members")]
    public class MemberController : Controller
    {
        private readonly MemberService _memberService;

        public MemberController(MemberService memberService)
        {
            _memberService = memberService;
        }

        // GET api/members
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Member>>> GetMembers()
        {
            return Ok(await _memberService.Get());
        }

        // GET api/members/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Member>> GetMember(int id)
        {
            var member = await _memberService.Get(id);
            if (member == null)
            {
                return NotFound();
            }
            return Ok(member);
        }

        // GET api/members/5
        [HttpGet("team/{teamId}")]
        public async Task<ActionResult<IEnumerable<Member>>> GetMemberOfaTeamAsync(int teamId)
        {
            var members = await _memberService.GetMembersOfaTeam(teamId);
            if (members == null)
            {
                return NotFound();
            }
            return Ok(members);
        }


        // POST api/members
        [HttpPost]
        public CreatedAtActionResult AddMember(Member member)
        {

            _memberService.Create(member);
            return CreatedAtAction(nameof(GetMember), new
            {
                id = member.Id
            }, member);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, Member teamÏn)
        {
            var team = _memberService.Get(id);

            if (team == null)
            {
                return NotFound();
            }
            _memberService.Update(id, teamÏn);

            return Ok();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var team = _memberService.Get(id);

            if (team == null)
            {
                return NotFound();
            }
            _memberService.Remove(id);
            return Ok();
        }
    }
}
