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
        private readonly IHubContext<AirportHub> _hubContext;
        private readonly List<Leg> Legs;
        private List<LegModel> _models;
        private System.Timers.Timer _timer;

        public AirportService(IAirPortRepository repo, IHubContext<AirportHub> hubContext)
        {
            _repository = repo;
            arrivals = new Arrivals(repo);
            departures = new Departures(repo);
        }


        public async void ProccessFlight(Flight flight)
        {
            try
            {
                await _repository.AddFlightAsync(flight);

                if (flight.IsDeparture) AddDepartureFlight(flight);
                else AddLandingFLight(flight);
            }
            catch (Exception ex)
            {

            }
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

        public Task<IEnumerable<Flight>> GetFlights()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Logger>> GetLogs()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<LegModel>> GetLegs()
        {
            return await _repository.GetLegModels();
        }
    }
}
