﻿namespace TournamentManager.Backend.Models
{
    public class TournamentManagerDatabaseSettings : ITournamentManagerDatabaseSettings
    {
        public string TeamsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface ITournamentManagerDatabaseSettings
    {
        string TeamsCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
