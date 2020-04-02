using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace TournamentManager.Controllers.DTO
{
    public class CreateMemberDto
    {
        [Required]
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
