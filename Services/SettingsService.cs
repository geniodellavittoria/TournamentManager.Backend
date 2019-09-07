using MongoDB.Driver;
using System.Threading.Tasks;
using TournamentManager.Backend.Models;

namespace TournamentManager.Backend.Services
{
    public class SettingsService
    {
        private readonly IMongoCollection<Settings> _settings;
        private const int settingsId = 1;

        public SettingsService(ITournamentManagerDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _settings = database.GetCollection<Settings>(settings.SettingsCollectionName);
        }

        public async Task<Settings> Get() =>
            await _settings.Find(setting => setting.Id == settingsId).FirstOrDefaultAsync();


        public async Task<Settings> Create(Settings settingsIn)
        {
            var settings = await Get();
            if (settings != null)
                return settingsIn;

            settingsIn.Id = settingsId;
            await _settings.InsertOneAsync(settingsIn);
            return settingsIn;
        }

        public void Update(Settings settingIn)
        {
            settingIn.Id = settingsId;
            _settings.ReplaceOneAsync(settings => settings.Id == settingsId, settingIn);
        }

    }
}
