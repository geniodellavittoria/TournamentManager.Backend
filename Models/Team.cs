using Newtonsoft.Json;

namespace TournamentManager.Backend.Models
{
    public class Team
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("isPaid")]
        public bool IsPaid { get; set; }

        [JsonProperty("groupId")]
        public int GroupId { get; set; }
    }
}
