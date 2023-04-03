using WebTerminalsServer.Dal;
using WebTerminalsServer.Logic;
using WebTerminalsServer.Models;

namespace WebTerminalsServer.Repositories
{
    public interface IAirPortRepository
    {
        Task SaveAsync();
        LegType LegToEnum(Leg leg);
        Task AddLegAsync(LegModel leg);
        Task RemoveLeg(LegModel leg);
        void UpdateLeg(LegModel leg);
        Task AddFlightAsync(Flight flight);
        Task RemoveFlight(Flight flight);
        Task UpdateFlight(Flight flight);
        Task AddLogAsync(Logger log);
        LegModel GetLegModel(int legNumber);
        IEnumerable<LegModel> GetLegModels();
        Task<IEnumerable<LegModel>> AsyncGetLegModels();
        void UpdateLegs(IEnumerable<LegModel> legModels);
        Task<Flight> GetFlightByCodeAsync(string code);
        Task AddOrUpdateLegWithFlightAsync(LegModel leg);
    }
}
