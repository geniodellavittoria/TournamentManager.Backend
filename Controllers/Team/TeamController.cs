using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TournamentManager.Backend.Controllers.Team;
using TournamentManager.Backend.Models;
using TournamentManager.Backend.Services;

namespace TournamentManager.Backend.Controllers
{
    [Produces("application/json")]
    [Route("api/teams")]
    public class TeamsController : Controller
    {
        private readonly TeamService _teamService;
        private readonly MemberService _memberService;

        public TeamsController(TeamService teamService, MemberService memberService)
        {
            this._teamService = teamService;
            this._memberService = memberService;
        }

        // GET api/teams
        [HttpGet]
        public async Task<IEnumerable<TeamDto>> GetTeamsAsync()
        {
            var members = await _memberService.Get();
            var teams = await _teamService.Get();

            var teamWithMembers = new List<TeamDto>();

            foreach (Models.Team team in teams)
            {
                var teamMembers = members.FindAll(x => x.TeamId == team.Id);
                teamWithMembers.Add(new TeamDto
                {
                    Id = team.Id,
                    Name = team.Name,
                    IsPaid = team.IsPaid,
                    Members = teamMembers
                });
            }

            return teamWithMembers;

        }

        // GET api/teams/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TeamDto>> GetTeam(int id)
        {
            var team = await _teamService.Get(id);

            if (team == null)
            {
                return NotFound();
            }

            var members = await _memberService.GetMembersOfTeam(id);
            return Ok(new TeamDto
            {
                Id = team.Id,
                IsPaid = team.IsPaid,
                Name = team.Name,
                Members = members
            });
        }


        // POST api/teams
        [HttpPost]
        public async Task<CreatedAtActionResult> AddTeamAsync(TeamDto dto)
        {
            Models.Team team = new Models.Team
            {
                IsPaid = dto.IsPaid,
                Name = dto.Name
            };
            await _teamService.Create(team);

            List<Member> members = dto.Members;
            members.ForEach(x =>
            {
                var newMember = new Member
                {
                    Id = x.Id,
                    Name = x.Name,
                    Lastname = x.Lastname,
                    Email = x.Email,
                    TeamId = team.Id
                };
                _memberService.Create(newMember);
            });

            return CreatedAtAction(nameof(GetTeam), new
            {
                id = team.Id
            }, dto);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, Models.Team teamÏn)
        {
            var team = _teamService.Get(id);

            if (team == null)
            {
                return NotFound();
            }
            _teamService.Update(id, teamÏn);

            return Ok();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var team = _teamService.Get(id);

            if (team == null)
            {
                return NotFound();
            }
            _teamService.Remove(id);
            return Ok();
        }
    }
}
