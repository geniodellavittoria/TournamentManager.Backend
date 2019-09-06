using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace TournamentManager.Backend.Models
{
    public class Member
    {
        [Required]
        public int Id { get; set; }

        [JsonProperty("TeamId")]
        public int TeamId { get; set; }

        [JsonProperty("Lastname")]
        public string Lastname { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Email")]
        public string Email { get; set; }
    }
}
