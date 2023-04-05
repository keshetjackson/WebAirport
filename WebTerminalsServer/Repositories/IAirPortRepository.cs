using WebTerminalsServer.Dal;
using WebTerminalsServer.Logic;
using WebTerminalsServer.Models;

namespace WebTerminalsServer.Repositories
{
    public interface IAirPortRepository
    {
        LegType LegToEnum(Leg leg);
        void UpdateLeg(LegModel? leg);
        Task AddFlightAsync(Flight flight);
        Task RemoveFlight(Flight flight);
        void AddLog(Logger log);
        LegModel GetLegModel(int legNumber);
        IEnumerable<LegModel> GetLegModels();
        Task<IEnumerable<LegModel>> AsyncGetLegModels();
        void UpdateLegs(IEnumerable<LegModel> legModels);
        Task<Flight> GetFlightByCodeAsync(string code);
    }
}
