using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using TournamentManager.Backend.Models;

namespace TournamentManager.Backend.Services
{
    public class TeamRepository
    {
        private readonly IMongoCollection<Team> _teams;

        public TeamRepository(ITournamentManagerDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _teams = database.GetCollection<Team>(settings.TeamsCollectionName);
        }

        public Task<List<Team>> Get() =>
            _teams.Find(team => true).ToListAsync();

        public Task<Team> Get(string id) =>
            _teams.Find<Team>(team => team.Id == id).FirstOrDefaultAsync();

        public Task<List<Team>> GetTeamsOfGroupAsync(string groupId) =>
            _teams.Find(team => team.GroupId == groupId).ToListAsync();

        public Task<List<Team>> GetTeamsWithGroupAsync() =>
            _teams.Find(team => team.GroupId != null || team.GroupId != "").ToListAsync();

        public async Task<Team> CreateAsync(Team team)
        {
            await _teams.InsertOneAsync(team).ConfigureAwait(false);
            return team;
        }

        public void Update(Team teamIn) =>
            _teams.ReplaceOneAsync(team => team.Id == teamIn.Id, teamIn);

        public void Remove(Team teamIn) =>
            _teams.DeleteOneAsync(team => team.Id == teamIn.Id);

        public void Remove(string id) =>
            _teams.DeleteOneAsync(team => team.Id == id);
    }
}

