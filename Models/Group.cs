using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace TournamentManager.Backend.Models
{
    public class Group
    {
        [Required]
        [JsonProperty("id")]
        public int Id { get; set; }


        [Required]
        [JsonProperty("groupName")]
        public string GroupName { get; set; }
    }
}
