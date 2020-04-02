using System;
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
        private readonly GameService _gameService;
        private readonly TeamService _teamService;
        private readonly GroupService _groupService;
        private readonly SettingsService _settingsService;

        public GameController(
            GameService gameService,
            TeamService teamService,
            SettingsService settingsService,
            GroupService groupService)
        {
            this._gameService = gameService;
            this._teamService = teamService;
            this._groupService = groupService;
            this._settingsService = settingsService;
        }

        // GET api/games
        [HttpGet]
        public async Task<IEnumerable<GroupGames>> GetGamesAsync()
        {

            var teamlist = await this._teamService.GetTeamsWithGroup();
            var games = await this._gameService.Get();
            var groups = await this._groupService.Get();
            var settings = await this._settingsService.Get();

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
                group.Games = CreateGamesForGroup(group.GroupId, settings, teams);
                _gameService.Create(group);
            }

            return games;
        }

        private List<Game> CreateGamesForGroup(string groupId, Settings settings, List<Team> teamList)
        {
            var games = new List<Game>();
            var teams = teamList;

            int numRounds = (settings.GroupSize - 1);
            int halfSize = settings.GroupSize / 2;

            teams = teams.Where(x => x.GroupId == groupId).ToList();
            if (teams.Count % 2 != 0)
            {
                teams.Add(new Team
                {
                    GroupId = groupId,
                    IsPaid = false,
                    Name = ""
                });
            }

            teams.AddRange(teams); // Copy all the elements.
            teams.RemoveAt(0); // To exclude the first team.

            int teamsSize = teams.Count;

            for (int round = 0; round < numRounds; round++)
            {
                Console.WriteLine("Round {0}", (round + 1));

                int teamIdx = round % teamsSize;

                Console.WriteLine("{0} vs {1}", teams[teamIdx], teamList[0]);
                games.Add(new Game
                {
                    HomeTeamName = teams[teamIdx].Name,
                    HomeTeamId = teams[teamIdx].Id,
                    AwayTeamName = teams[0].Name,
                    AwayTeamId = teams[0].Id
                });

                for (int idx = 1; idx < halfSize; idx++)
                {
                    int firstTeam = (round + idx) % teamsSize;
                    int secondTeam = (round + teamsSize - idx) % teamsSize;
                    Console.WriteLine("{0} vs {1}", teams[firstTeam], teams[secondTeam]);
                    games.Add(new Game
                    {
                        HomeTeamId = teams[firstTeam].Id,
                        HomeTeamName = teams[firstTeam].Name,
                        AwayTeamId = teams[secondTeam].Id,
                        AwayTeamName = teams[secondTeam].Name
                    });
                }
            }

            return games;
        }




    }
}
