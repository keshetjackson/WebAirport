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
    public class FlightsController : ControllerBase
    {
        private readonly IAirportService _airportService;
        private readonly IAirPortRepository _airportRepository;


        public FlightsController(IAirportService airport, IAirPortRepository airportRepository)
        {
            this._airportService = airport;
            this._airportRepository = airportRepository;
        }

        [HttpGet]
        public async Task<List<LegModel>> Get() => (List<LegModel>)await _airportRepository.AsyncGetLegModels();

        [HttpPost]
        public async Task AddFlight(Flight flight)
        {
             _airportService.ProccessFlight(flight);
        }
    }
}