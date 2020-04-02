using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TournamentManager.Backend.Models;
using TournamentManager.Backend.Services;

namespace TournamentManager.Backend.Controllers
{
    [Produces("application/json")]
    [Route("api/games")]
    public class GameController : Controller
    {
        private readonly GameRepository _gameRepo;
        private readonly TeamRepository _teamRepo;
        private readonly GroupRepository _groupRepo;
        private readonly SettingsRepository _settingsRepo;
        private readonly IGameService gameService;

        public GameController(
            GameRepository gameRepo,
            TeamRepository teamRepo,
            SettingsRepository settingsRepo,
            GroupRepository groupRepo,
            IGameService gameService)
        {
            this._gameRepo = gameRepo;
            this._teamRepo = teamRepo;
            this._groupRepo = groupRepo;
            this._settingsRepo = settingsRepo;
            this.gameService = gameService;
        }

        // GET api/games
        [HttpGet]
        public async Task<IEnumerable<GroupGames>> GetGamesAsync()
        {

            var teamlist = await this._teamRepo.GetTeamsWithGroup();
            var games = await this._gameRepo.Get();
            var groups = await this._groupRepo.Get();
            var settings = await this._settingsRepo.Get();

            groups.ForEach(group =>
             {
                 if (!games.Select(x => x.GroupId).ToList().Contains(group.Id))
                 {
                     games.Add(new GroupGames()
                     {
                         Games = new List<Game>(),
                         GroupName = group.Name,
                         GroupId = group.Id
                     });
                 }
             });

            foreach (var group in games.Where(x => x.Games == null || !x.Games.Any()).ToList())
            {
                var teams = teamlist.Where(x => x.GroupId == group.GroupId).ToList();
                if (!teams.Any())
                    continue;
                group.Games = gameService.CreateGamesForGroup(group.GroupId, settings, teams);
                _gameRepo.Create(group);
            }

            return games;
        }
    }
}
