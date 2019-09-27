using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using TournamentManager.Backend.Models;

namespace TournamentManager.Backend.Services
{
    public class GroupService
    {
        private readonly IMongoCollection<Group> _groups;

        public GroupService(ITournamentManagerDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _groups = database.GetCollection<Group>(settings.GroupsCollectionName);
        }

        public async Task<List<Group>> Get() =>
            await _groups.Find(group => true).ToListAsync();

        public async Task<List<int>> GetGroupIds()
        {
            var fields = Builders<Group>.Projection.Include(p => p.Id);
            return await _groups.Find(group => true).Project<int>(fields).ToListAsync();
        }

        public async Task<Group> Get(string id)
        {
            return await _groups.Find(group => group.Id == id).FirstOrDefaultAsync();
        }

        public Group Create(Group group)
        {
            _groups.InsertOneAsync(group);
            return group;
        }

        public void Update(string id, Group groupIn) =>
            _groups.ReplaceOneAsync(group => group.Id == id, groupIn);

        public void Remove(string id) =>
            _groups.DeleteOneAsync(group => group.Id == id);
    }
}

