using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace TournamentManager.Backend.Controllers
{
    public class GameDto
    {
        [Required]
        [JsonProperty("id")]
        public long Id { get; set; }

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

