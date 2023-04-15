using WebTerminalsServer.Models;

namespace WebTerminalsServer.Services
{
    public interface IAirportService
    {
        void ProccessFlight(Flight flight);
        void AddLandingFLight(Flight flight);
        void AddDepartureFlight(Flight flight);
    }
}
