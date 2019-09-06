using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TournamentManager.Backend.Models;
using TournamentManager.Backend.Services;

namespace TournamentManager.Backend.Controllers
{
    [Route("api/teams")]
    public class TeamsController : Controller
    {
        private readonly TeamService _teamService;

        public TeamsController(TeamService teamService)
        {
            this._teamService = teamService;
        }

        // GET api/teams
        [HttpGet]
        public ActionResult<IEnumerable<Team>> GetTeams()
        {
            return _teamService.Get();
        }

        // GET api/teams/5
        [HttpGet("{id}")]
        public ActionResult<Team> GetTeam(int id)
        {
            var team = _teamService.Get(id);
            if (team == null)
            {
                return NotFound();
            }
            return team;
        }

        // POST api/teams
        [HttpPost]
        public CreatedAtActionResult AddTeam(Team team)
        {
            _teamService.Create(team);
            return CreatedAtAction(nameof(GetTeam), new
            {
                id = team.Id
            }, team);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, Team teamÏn)
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
