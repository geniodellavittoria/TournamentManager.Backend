using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TournamentManager.Backend.Models;

namespace TournamentManager.Backend.Controllers.Team
{
    public class TeamDto
    {
        [Required]
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("isPaid")]
        public bool IsPaid { get; set; }

        [JsonProperty("members")]
        public List<Member> Members { get; set; }
    }
}

