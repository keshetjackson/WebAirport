using Microsoft.AspNetCore.SignalR;
using WebTerminalsServer.Models;

namespace WebTerminalsServer.Hubs
{
    public class AirportHub : Hub
    {
        public async Task NotifyLegUpdated()
        {
            await Clients.All.SendAsync("LegUpdated");
        }

        public async Task NotifyLogAdded(Logger log)
        {
            await Clients.All.SendAsync("LogAdded");
        }
    }
}
