using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace TournamentManager.Controllers.DTO
{
    public class CreateTeamMemberDto
    {
        [Required]
        [JsonProperty("lastname")]
        public string Lastname { get; set; }

        [Required]
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
