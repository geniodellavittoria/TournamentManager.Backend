using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using TournamentManager.Backend.Models;

namespace TournamentManager.Backend.Services
{
    public class GameService
    {
        private readonly IMongoCollection<Game> _games;

        public GameService(ITournamentManagerDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _games = database.GetCollection<Game>(settings.GamesCollectionName);
        }

        public async Task<ICollection<Game>> Get() =>
            await _games.Find(game => true).ToListAsync();

        public async Task<Game> Get(int id) => await _games.Find(game => game.Id == id).FirstOrDefaultAsync();

        public async Task<List<Game>> GetGamesOfTeam(string teamId) =>
            await _games.Find(game => game.HomeTeamId == teamId || game.AwayTeamId == teamId).ToListAsync();

        public async Task<List<Game>> GetGamesOfAGroup(List<string> teamIds) =>
            await _games.Find(game => teamIds.Contains(game.HomeTeamId) || teamIds.Contains(game.AwayTeamId)).ToListAsync();

        public Game Create(Game game)
        {
            _games.InsertOneAsync(game);
            return game;
        }

        public void Update(int id, Game gameIn) =>
            _games.ReplaceOneAsync(game => game.Id == id, gameIn);

        public void Remove(int id) =>
            _games.DeleteOneAsync(game => game.Id == id);
    }
}

