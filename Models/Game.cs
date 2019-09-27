using Newtonsoft.Json;

namespace TournamentManager.Backend.Models
{
    public class Game
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("gameNumber")]
        public long GameNumber { get; set; }

        [JsonProperty("homeTeamId")]
        public string HomeTeamId { get; set; }

        [JsonProperty("awayTeamId")]
        public string AwayTeamId { get; set; }

        [JsonProperty("homeTeamScore")]
        public int HomeTeamScore { get; set; }

        [JsonProperty("awayTeamScore")]
        public int AwayTeamScore { get; set; }

    }
}
