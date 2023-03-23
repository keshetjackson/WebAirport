using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebTerminalsServer.Dal;
using WebTerminalsServer.Logic;
using WebTerminalsServer.Models;

namespace WebTerminalsServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private readonly IAirport _airport;

        public FlightsController(IAirport airport)
        {
            this._airport = airport;
        }

        [HttpGet]
        public async Task<List<Flight>> Get() => (List<Flight>)await _airport.GetFlights();

        [HttpPost]
        public async Task AddFlight(Flight flight)
        {
             _airport.ProccessFlight(flight);
        }
    }
}