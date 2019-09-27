using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace TournamentManager.Backend.Models
{
    public class UpdateMemberDto
    {
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonProperty("teamId")]
        public string TeamId { get; set; }

        [JsonProperty("lastname")]
        public string Lastname { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }
    }
}
