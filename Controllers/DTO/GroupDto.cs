using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TournamentManager.Backend.Controllers.DTO
{
    public class GroupDto
    {
        [Required]
        [JsonProperty("id")]
        public int Id { get; set; }


        [Required]
        [JsonProperty("groupName")]
        public string GroupName { get; set; }

        [JsonProperty("teams")]
        public List<TeamDto> Teams { get; set; }
    }
}
