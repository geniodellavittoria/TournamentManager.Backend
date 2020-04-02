using Newtonsoft.Json;

namespace TournamentManager.Backend.Models
{
    public class Game
    {

        [JsonProperty("homeTeamId")]
        public string HomeTeamId { get; set; }

        [JsonProperty("awayTeamId")]
        public string AwayTeamId { get; set; }

        [JsonProperty("homeTeamName")]
        public string HomeTeamName { get; set; }

        [JsonProperty("awayTeamName")]
        public string AwayTeamName { get; set; }

        [JsonProperty("homeTeamScore")]
        public int HomeTeamScore { get; set; }

        [JsonProperty("awayTeamScore")]
        public int AwayTeamScore { get; set; }

    }
}
