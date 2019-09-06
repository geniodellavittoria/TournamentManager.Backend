﻿using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace TournamentManager.Backend.Models
{
    public class Team
    {
        [Required]

        public int Id { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("IsPaid")]
        public bool IsPaid { get; set; }
    }
}
