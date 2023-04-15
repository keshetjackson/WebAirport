using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebTerminalsServer.Dal;
using WebTerminalsServer.Models;
using WebTerminalsServer.Repositories;
using WebTerminalsServer.Services;

namespace WebTerminalsServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirportController : ControllerBase
    {
        private readonly IAirportService _airportService;
        private readonly IAirPortRepository _airportRepository;


        public AirportController(IAirportService airport, IAirPortRepository airportRepository)
        {
            this._airportService = airport;
            this._airportRepository = airportRepository;
        }

        [HttpGet("Legs")]
        public async Task<List<LegModel>> GetLegs() => (List<LegModel>)await _airportRepository.GetLegModels();
        [HttpGet("Logs")]
        public async Task<List<Logger>> GetLogs() => (List<Logger>)await _airportRepository.GetLogs();

        [HttpPost]
        public async Task AddFlight(Flight flight) => _airportService.ProccessFlight(flight);

        [HttpGet("Leg")]
        public async Task<IEnumerable<Logger>> GetLog(int id) => await _airportRepository.GetLog(id);

        

    }
}