using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebTerminalsServer.Dal;
using WebTerminalsServer.Logic;
using WebTerminalsServer.Models;
using WebTerminalsServer.Services;

namespace WebTerminalsServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private readonly IAirport _airport;
        private readonly IAirPortRepository _airportRepository;


        public FlightsController(IAirport airport, IAirPortRepository airportRepository)
        {
            this._airport = airport;
            this._airportRepository = airportRepository;
        }

        [HttpGet]
        public async Task<List<LegModel>> Get() => (List<LegModel>)await _airportRepository.AsyncGetLegModels();

        [HttpPost]
        public async Task AddFlight(Flight flight)
        {
             _airport.ProccessFlight(flight);
        }
    }
}