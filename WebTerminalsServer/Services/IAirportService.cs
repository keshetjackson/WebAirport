using WebTerminalsServer.Dal;
using WebTerminalsServer.Logic;
using WebTerminalsServer.Models;

namespace WebTerminalsServer.Services
{
    public interface IAirPortService
    {
        Task SaveAsync();
        Task AddLegAsync(Leg leg);
        Task RemoveLeg(Leg leg);
        Task UpdateLeg(Leg leg);
        Task AddFlightAsync(Flight flight);
        Task RemoveFlight(Flight flight);
        Task UpdateFlight(Flight flight);
        Task AddLogAsync(Logger log);
    }
}
