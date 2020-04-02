using System;
using System.Collections.Generic;
using System.Linq;
using TournamentManager.Backend.Models;

namespace TournamentManager.Backend.Services
{
    public class GameService : IGameService
    {
        public List<Game> CreateGamesForGroup(string groupId, Settings settings, List<Team> teamList)
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
