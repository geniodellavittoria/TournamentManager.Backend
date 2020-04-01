using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TournamentManager.Backend.Models;
using TournamentManager.Backend.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TournamentManager.Backend.Controllers
{
    [Route("api/settings")]
    public class SettingsController : Controller
    {
        private readonly SettingsService _settingsService;

        public SettingsController(SettingsService settingsService)
        {
            this._settingsService = settingsService;
        }

        // GET: api/settings
        [HttpGet]
        public async Task<Settings> Get()
        {
            return await _settingsService.Get();
        }

        // PUT api/settings
        [HttpPut()]
        public void Put(Settings settingsIn)
        {
            _settingsService.Update(settingsIn);
        }
    }
}
