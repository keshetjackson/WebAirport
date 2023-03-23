using WebTerminalsServer.Dal;
using WebTerminalsServer.Logic;
using WebTerminalsServer.Models;

namespace WebTerminalsServer.Services
{
    public class AirportService : IAirPortService
    {
        private readonly DataContext _dataContext;

        public AirportService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task AddFlightAsync(Flight flight)
        {
            await _dataContext.flights.AddAsync(flight);
            await _dataContext.SaveChangesAsync();
        }

        public async Task AddLegAsync(Leg leg)
        {
            await _dataContext.legs.AddAsync(leg);
            await _dataContext.SaveChangesAsync();
        }

        public async Task AddLogAsync(Logger log)
        {
            await _dataContext.logs.AddAsync(log);
            await _dataContext.SaveChangesAsync();
        }

        public async Task RemoveFlight(Flight flight)
        {
             _dataContext.flights.Remove(flight);
            await _dataContext.SaveChangesAsync();
        }

        public async Task RemoveLeg(Leg leg)
        {
            _dataContext.legs.Remove(leg);
            await _dataContext.SaveChangesAsync();
        }

        public Task SaveAsync()
        {
            throw new NotImplementedException();
        }

        public async Task UpdateFlight(Flight flight)
        {
            _dataContext.flights.Update(flight);
            await _dataContext.SaveChangesAsync();
        }

        public async Task UpdateLeg(Leg leg)
        {
            _dataContext.legs.Update(leg);
            await _dataContext.SaveChangesAsync();
        }
    }
}
