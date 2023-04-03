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
        //private readonly PriorityQueue<Flight, bool> departures;
        //private readonly PriorityQueue<Flight, bool> arrivals;

        public AirportService(IAirPortRepository repo, IHubContext<AirportHub> hubContext)
        {
            _repository = repo;
            arrivals = new Arrivals(repo);
            departures = new Departures(repo);
        }

        //public AirportService(IAirPortRepository repo, IHubContext<AirportHub> hubContext)
        //{
        //    _repository = repo;
        //    _hubContext = hubContext;
        //    Legs = new List<Leg>()
        //    {
        //        LegFactory<Leg1>.GetInstance(),
        //        LegFactory<Leg2>.GetInstance(),
        //        LegFactory<Leg3>.GetInstance(),
        //        LegFactory<Leg4>.GetInstance(),
        //        LegFactory<Leg5>.GetInstance(),
        //        LegFactory<Leg6>.GetInstance(),
        //        LegFactory<Leg7>.GetInstance(),
        //        LegFactory<Leg8>.GetInstance(),
        //        LegFactory<Leg9>.GetInstance(),
        //    };
        //    _models = new List<LegModel>();
        //    arrivals = new Arrivals();
        //    departures = new Departures();
        //    InitLegModels();
        //    _timer = new System.Timers.Timer();
        //    InitClientUpdates();
        //}

        private void InitClientUpdates()
        {
            _timer.Interval = 1000; // 0.5 sec
            _timer.Elapsed += UpdateClient;
            _timer.Start();
        }

        private async void UpdateClient(object sender, ElapsedEventArgs e)
        {
            _repository.UpdateLegs(_models);
            await _hubContext.Clients.All.SendAsync("GetLegs", _models);
        }

        private void InitLegModels()
        {
            _models = _repository.GetLegModels().ToList();
            Legs.ForEach(l => l.legModel = _models.First(lm => lm.Number == l.Number));
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
            return await _repository.AsyncGetLegModels();
        }
    }
}
