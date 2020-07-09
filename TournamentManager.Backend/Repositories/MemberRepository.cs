using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using TournamentManager.Backend.Models;

namespace TournamentManager.Backend.Services
{
    public class MemberRepository
    {
        private readonly IMongoCollection<Member> _members;

        public MemberRepository(ITournamentManagerDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _members = database.GetCollection<Member>(settings.MembersCollectionName);
        }

        public Task<List<Member>> GetAsync() =>
            _members.Find(member => true).ToListAsync();

        public Task<Member> GetAsync(string id) => _members.Find(member => member.Id == id).FirstOrDefaultAsync();

        public Task<List<Member>> GetMembersOfTeamAsync(string teamId) =>
            _members.Find(member => member.TeamId == teamId).ToListAsync();

        public List<Member> AddMembers(List<Member> members)
        {
            _members.InsertMany(members);
            return members;
        }
        public Member Create(Member member)
        {
            _members.InsertOne(member);
            return member;
        }

        public Task<ReplaceOneResult> UpdateAsync(string id, Member memberIn) =>
            _members.ReplaceOneAsync(member => member.Id == id, memberIn);

        public void Remove(string id) =>
            _members.DeleteOneAsync(member => member.Id == id);
    }
}

