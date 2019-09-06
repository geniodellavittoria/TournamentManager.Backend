using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using TournamentManager.Backend.Models;

namespace TournamentManager.Backend.Services
{
    public class MemberService
    {
        private readonly IMongoCollection<Member> _members;

        public MemberService(ITournamentManagerDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _members = database.GetCollection<Member>(settings.MembersCollectionName);
        }

        public async Task<List<Member>> Get() =>
            await _members.Find(member => true).ToListAsync();

        public async Task<Member> Get(int id) => await _members.Find(member => member.Id == id).FirstOrDefaultAsync();

        public async Task<ICollection<Member>> GetMembersOfaTeam(int teamId) =>
            await _members.Find(member => member.TeamId == teamId).ToListAsync();

        public Member Create(Member member)
        {
            _members.InsertOneAsync(member);
            return member;
        }

        public void Update(int id, Member memberIn) =>
            _members.ReplaceOneAsync(member => member.Id == id, memberIn);

        public void Remove(Member memberIn) =>
            _members.DeleteOneAsync(member => member.Id == memberIn.Id);

        public void Remove(int id) =>
            _members.DeleteOneAsync(member => member.Id == id);
    }
}

