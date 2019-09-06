using Newtonsoft.Json;

namespace TournamentManager.Backend.Models
{
    public class Team
    {
        public int Id { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("IsPaid")]
        public bool IsPaid { get; set; }
    }
}
