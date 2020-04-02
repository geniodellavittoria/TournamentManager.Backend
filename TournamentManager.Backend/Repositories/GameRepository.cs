using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using TournamentManager.Backend.Models;

namespace TournamentManager.Backend.Services
{
    public class GameRepository
    {
        private readonly IMongoCollection<GroupGames> _games;

        public GameRepository(ITournamentManagerDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _games = database.GetCollection<GroupGames>(settings.GamesCollectionName);
        }

        public async Task<ICollection<GroupGames>> Get() =>
            await _games.Find(groupGames => true).ToListAsync();

        public async Task<List<GroupGames>> GetGamesOfAGroup(string groupId) =>
            await _games.Find(groupGame => groupGame.GroupId == groupId).ToListAsync();

        public GroupGames Create(GroupGames groupGames)
        {
            _games.InsertOneAsync(groupGames);
            return groupGames;
        }
    }
}

