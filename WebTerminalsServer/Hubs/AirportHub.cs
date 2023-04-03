using Microsoft.AspNetCore.SignalR;
using WebTerminalsServer.Models;

namespace WebTerminalsServer.Hubs
{
    public class AirportHub : Hub
    {
        public async Task SendLegs(IEnumerable<LegModel> legs)
        {
            await Clients.All.SendAsync("GetLegs", legs);
        }

        public async Task SendFlights(IEnumerable<Flight> flights)
        {
            await Clients.All.SendAsync("GetFlights", flights);
        }

        public async Task SendLogs(IEnumerable<Logger> logs)
        {
            await Clients.All.SendAsync("GetLogs", logs);
        }
    }
}
