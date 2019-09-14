using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace TournamentManager.Backend.Models
{
    public class Member
    {
        [Required]
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("teamId")]
        public int TeamId { get; set; }

        [JsonProperty("lastname")]
        public string Lastname { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }
    }
}
