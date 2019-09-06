using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using TournamentManager.Backend.Models;

namespace TournamentManager.Backend.Services
{
    public class TeamService
    {
        private readonly IMongoCollection<Team> _teams;

        public TeamService(ITournamentManagerDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _teams = database.GetCollection<Team>(settings.TeamsCollectionName);
        }

        public async Task<List<Team>> Get() =>
            await _teams.Find(team => true).ToListAsync();

        public async Task<Team> Get(int id) =>
            await _teams.Find<Team>(team => team.Id == id).FirstOrDefaultAsync();

        public async Task<Team> Create(Team team)
        {
            await _teams.InsertOneAsync(team);
            return team;
        }

        public void Update(int id, Team teamIn) =>
            _teams.ReplaceOneAsync(team => team.Id == id, teamIn);

        public void Remove(Team teamIn) =>
            _teams.DeleteOneAsync(team => team.Id == teamIn.Id);

        public void Remove(int id) =>
            _teams.DeleteOneAsync(team => team.Id == id);
    }
}

