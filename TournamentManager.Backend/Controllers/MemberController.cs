using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TournamentManager.Backend.Models;
using TournamentManager.Backend.Services;
using TournamentManager.Controllers.DTO;

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
        public async Task<ActionResult<Member>> GetMember(string id)
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
        public async Task<ActionResult<IEnumerable<Member>>> GetMemberOfaTeamAsync(string teamId)
        {
            var members = await _memberService.GetMembersOfTeam(teamId);
            if (members == null)
            {
                return NotFound();
            }
            return Ok(members);
        }


        // POST api/members
        [HttpPost]
        public CreatedAtActionResult AddMember(CreateMemberDto dto)
        {

            var createdMember = _memberService.Create(new Member
            {
                Email = dto.Email,
                Name = dto.Name,
                Lastname = dto.Lastname,
                TeamId = dto.TeamId
            });
            return CreatedAtAction(nameof(AddMember), new
            {
                id = createdMember.Id
            }, createdMember);
        }


        // PUT api/members/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMember(string id, [FromBody]UpdateMemberDto memberIn)
        {
            var member = await _memberService.Get(id);

            if (member == null)
            {
                return NotFound();
            }

            var updatedMember = new Member
            {
                Id = id,
                Name = memberIn.Name,
                Lastname = memberIn.Lastname,
                Email = memberIn.Email,
                TeamId = memberIn.TeamId
            };

            var result = await _memberService.Update(id, updatedMember);
            if (result.IsAcknowledged)
            {
                return Ok();
            }
            else
            {
                return Conflict();
            }
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
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
