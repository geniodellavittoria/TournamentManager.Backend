using Newtonsoft.Json;

namespace TournamentManager.Backend.Models
{
    public class Settings
    {
        public int Id { get; set; }
        [JsonProperty("teamSize")]
        public int TeamSize { get; set; }

        [JsonProperty("groupSize")]
        public int GroupSize { get; set; }
    }
}
