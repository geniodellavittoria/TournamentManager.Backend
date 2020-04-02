using System.Collections.Generic;
using TournamentManager.Backend.Models;

namespace TournamentManager.Backend.Services
{
    public interface IGameService
    {
        List<Game> CreateGamesForGroup(string groupId, Settings settings, List<Team> teamList);
    }
}
