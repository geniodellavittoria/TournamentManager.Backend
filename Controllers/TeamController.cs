using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        private readonly GroupService _groupService;

        public TeamsController(TeamService teamService,
            MemberService memberService,
            GroupService groupService)
        {
            this._teamService = teamService;
            this._memberService = memberService;
            this._groupService = groupService;
        }

        // GET api/teams
        [HttpGet]
        public async Task<IEnumerable<TeamDto>> GetTeamsAsync()
        {
            var members = await _memberService.Get();
            var teams = await _teamService.Get();
            var groups = await _groupService.Get();

            var teamWithMembers = new List<TeamDto>();

            foreach (Models.Team team in teams)
            {
                var teamMembers = members.FindAll(x => x.TeamId == team.Id);
                var group = groups.Find(x => x.Id == team.GroupId);
                teamWithMembers.Add(new TeamDto
                {
                    Id = team.Id,
                    Name = team.Name,
                    IsPaid = team.IsPaid,
                    Members = teamMembers,
                    Group = group
                });
            }

            return teamWithMembers;

        }

        // GET api/teams/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TeamDto>> GetTeam(string id)
        {
            var team = await _teamService.Get(id);

            if (team == null)
            {
                return NotFound();
            }
            var group = await _groupService.Get(team.GroupId);
            var members = await _memberService.GetMembersOfTeam(id);
            return Ok(new TeamDto
            {
                Id = team.Id,
                IsPaid = team.IsPaid,
                Name = team.Name,
                Group = group,
                Members = members
            });
        }

        // GET api/teams/group/5
        [HttpGet("group/{id}")]
        public async Task<ActionResult<List<TeamDto>>> GetTeamsOfGroup(string groupId)
        {
            List<Team> teams = null;
            List<Member> members = null;

            teams = await _teamService.GetTeamsOfGroup(groupId);
            members = await _memberService.Get();

            if (teams == null)
            {
                return NotFound();
            }

            teams.ForEach(team => new TeamDto
            {
                Id = team.Id,
                IsPaid = team.IsPaid,
                Name = team.Name,
                Members = members.FindAll(member => member.TeamId == team.Id)
            });

            return Ok(teams);



        }

        // POST api/teams
        [HttpPost]
        public async Task<CreatedAtActionResult> AddTeamAsync(CreateTeamDto dto)
        {
            Models.Team team = new Models.Team
            {
                IsPaid = dto.IsPaid,
                GroupId = dto.GroupId,
                Name = dto.Name
            };
            var createdTeam = await _teamService.Create(team);

            return CreatedAtAction(nameof(AddTeamAsync), new
            {
                id = createdTeam.Id
            }, dto);
        }

        // PUT api/teams/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTeamAsync(string id, [FromBody] UpdateTeamDto teamIn)
        {
            var team = await _teamService.Get(id);

            if (team == null)
            {
                return NotFound();
            }

            var newTeam = new Team
            {
                Id = id,
                Name = teamIn.Name,
                GroupId = teamIn.GroupId,
                IsPaid = teamIn.IsPaid
            };

            _teamService.Update(newTeam);
            return NoContent();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
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
