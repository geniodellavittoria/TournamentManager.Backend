using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TournamentManager.Backend.Services;

namespace TournamentManager.Backend.Controllers
{
    [Produces("application/json")]
    [Route("api/games")]
    public class GameController : Controller
    {
        private readonly GameService _gameService;
        private readonly TeamService _teamService;

        public GameController(
            GameService gameService,
            TeamService teamService)
        {
            this._gameService = gameService;
            this._teamService = teamService;
        }

        // GET api/games
        [HttpGet]
        public async Task<IEnumerable<GameDto>> GetGamesAsync()
        {
            var games = await this._gameService.Get();
            var teams = await this._teamService.Get();

            return games.Select(game => new GameDto
            {
                Id = game.Id,
                AwayTeamName = GetTeamName(game.AwayTeamId, teams),
                AwayTeamScore = game.AwayTeamScore,
                HomeTeamName = GetTeamName(game.HomeTeamId, teams),
                HomeTeamScore = game.HomeTeamScore,
            });
        }


        private static string GetTeamName(string teamId, List<Models.Team> teams)
        {
            return teams.Where(team => team.Id == teamId).Select(team => team.Name).FirstOrDefault();
        }
    }
}
