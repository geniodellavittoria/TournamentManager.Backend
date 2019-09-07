using Newtonsoft.Json;

namespace TournamentManager.Backend.Models
{
    public class Settings
    {
        public int Id { get; set; }
        [JsonProperty("TeamSize")]
        public int TeamSize { get; set; }

        [JsonProperty("GroupSize")]
        public int GroupSize { get; set; }
    }
}
