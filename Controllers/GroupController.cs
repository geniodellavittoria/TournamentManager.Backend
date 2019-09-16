using Microsoft.AspNetCore.Mvc;
using TournamentManager.Backend.Services;

namespace TournamentManager.Backend.Controllers
{
    [Route("api/groups")]
    public class GroupController : Controller
    {

        private readonly GroupService _groupService;
        private readonly TeamService _teamService;

        public GroupController(
            GroupService groupService,
            TeamService teamService)
        {
            this._groupService = groupService;
            this._teamService = teamService;
        }


    }
}
