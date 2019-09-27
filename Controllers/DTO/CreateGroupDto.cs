using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace TournamentManager.Backend.Controllers.DTO
{
    public class CreateGroupDto
    {

        [Required]
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
