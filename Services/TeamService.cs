using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
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

        public List<Team> Get() =>
            _teams.Find(team => true).ToList();

        public Team Get(int id) =>
            _teams.Find<Team>(team => team.Id == id).FirstOrDefault();

        public Team Create(Team team)
        {
            _teams.InsertOne(team);
            return team;
        }

        public void Update(int id, Team teamIn) =>
            _teams.ReplaceOne(team => team.Id == id, teamIn);

        public void Remove(Team teamIn) =>
            _teams.DeleteOne(team => team.Id == teamIn.Id);

        public void Remove(int id) =>
            _teams.DeleteOne(team => team.Id == id);
    }
}

