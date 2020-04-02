using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TournamentManager.Backend.Controllers.DTO;
using TournamentManager.Backend.Models;
using TournamentManager.Backend.Services;

namespace TournamentManager.Backend.Controllers
{
    [Produces("application/json")]
    [Route("api/groups")]
    public class GroupController : Controller
    {

        private readonly GroupRepository _groupService;

        public GroupController(
            GroupRepository groupService)
        {
            _groupService = groupService;
        }

        // GET api/groups
        [HttpGet]
        public async Task<List<Group>> GetGroups()
        {
            return await _groupService.Get();
        }

        // GET api/groups
        [HttpGet("{id}")]
        public async Task<Group> GetGroup(string id)
        {
            return await _groupService.Get(id);
        }

        // POST api/groups
        [HttpPost]
        public CreatedAtActionResult AddGroup(CreateGroupDto group)
        {
            var createdGroup = _groupService.Create(new Group
            {
                Name = group.Name
            });
            return CreatedAtAction(nameof(AddGroup), new
            {
                id = createdGroup.Id
            }, createdGroup);
        }

    }
}
