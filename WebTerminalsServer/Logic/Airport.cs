using Microsoft.EntityFrameworkCore;
using WebTerminalsServer.Dal;
using WebTerminalsServer.Models;
using WebTerminalsServer.Services;

namespace WebTerminalsServer.Logic
{
    public class Airport : IAirport
    {
         Arrivals arrivals { get; set; }
         Departures departures { get; set; }
        //private readonly DataContext? _dbcontext;
        private readonly IAirPortService _service;
         
        public Airport(IAirPortService service)
        {
            _service = service;
            //_dbcontext = dbcontext;
            this.arrivals = new Arrivals(service);
            this.departures= new Departures(service);
        }

        public async void ProccessFlight(Flight flight)
        {
            if (flight.IsDeparture) AddDepartureFight(flight);
            else AddLandingFLight(flight);
        }

        public void AddLandingFLight(Flight flight)
        {
            arrivals.AddFlight(flight);
        }

        public void AddDepartureFight(Flight flight)
        {            
            departures.AddFlight(flight);
        }

        public Task<IEnumerable<Flight>> GetFlights()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Logger>> GetLogs()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Leg>> GetLegs()
        {
            throw new NotImplementedException();
        }
    }
}
