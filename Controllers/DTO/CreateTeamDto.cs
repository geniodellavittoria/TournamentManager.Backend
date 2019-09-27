﻿using Newtonsoft.Json;

namespace TournamentManager.Backend.Controllers
{
    public class CreateTeamDto
    {

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("isPaid")]
        public bool IsPaid { get; set; }

        [JsonProperty("groupId")]
        public string GroupId { get; set; }


    }
}

