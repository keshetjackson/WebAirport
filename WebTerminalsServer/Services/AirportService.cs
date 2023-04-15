using Microsoft.AspNetCore.SignalR;
using System.Timers;
using WebTerminalsServer.Hubs;
using WebTerminalsServer.Logic;
using WebTerminalsServer.Models;
using WebTerminalsServer.Repositories;

namespace WebTerminalsServer.Services
{
    public class AirportService : IAirportService
    {
        Arrivals arrivals { get; set; }
        Departures departures { get; set; }

        private readonly IAirPortRepository _repository;

        public AirportService(IAirPortRepository repo)
        {
            _repository = repo;
            arrivals = new Arrivals(repo);
            departures = new Departures(repo);
        }


        public async void ProccessFlight(Flight flight)
        {         
                await _repository.AddFlightAsync(flight);

                if (flight.IsDeparture) AddDepartureFlight(flight);
                else AddLandingFLight(flight);          
        }

        public void AddLandingFLight(Flight flight)
        {
            arrivals.AddFlight(flight);
        }

        //public void ChangeNextLeg(Leg leg)
        //{
        //    if (leg is Leg6) leg.NextLeg = Legs.Where(l => l is Leg7).First();
        //    else leg.NextLeg = Legs.Where(l => l is Leg6).First();
        //}

        public void AddDepartureFlight(Flight flight)
        {
            departures.AddFlight(flight);
        }

    }
}
