using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using WebTerminalsServer.Dal;
using WebTerminalsServer.Hubs;
using WebTerminalsServer.Logic;
using WebTerminalsServer.Logic.Legs;
using WebTerminalsServer.Models;

namespace WebTerminalsServer.Repositories
{
    public class AirportRepository : IAirPortRepository
    {
        private readonly IDbContextFactory<DataContext> _contextFactory;
        private readonly IHubContext<AirportHub> _hubContext;


        public AirportRepository(IDbContextFactory<DataContext> contextFactory, IHubContext<AirportHub> hubContext)
        {
            _contextFactory = contextFactory;
            _hubContext = hubContext;
        }


        public LegType LegToEnum(Leg leg)
        {
            switch (leg)
            {
                case Leg1:
                    return LegType.One;
                case Leg2:
                    return LegType.Two;
                case Leg3:
                    return LegType.Three;
                case Leg4:
                    return LegType.Four;
                case Leg5:
                    return LegType.Five;
                case Leg6:
                    return LegType.Six;
                case Leg7:
                    return LegType.Seven;
                case Leg8:
                    return LegType.Eight;
                case Leg9:
                    return LegType.Nine;
                default: return LegType.None;
            }
        }

        public async void UpdateLeg(LegModel? leg)
        {           
                if (leg.Flight == null)
                {
                    using (var context = _contextFactory.CreateDbContext())
                    {
                        var model  = await context.Legs.FirstAsync(l => l.Number == leg.Number);
                        model.Flight = null;
                        model.FlightId = null;
                        await context.SaveChangesAsync();
                    }
                }
                else
                {
                    using (var context = _contextFactory.CreateDbContext())
                    {
                        context.Legs.Update(leg);
                        await context.SaveChangesAsync();
                    }
                }
                await _hubContext.Clients.All.SendAsync("LegUpdated");          

        }

        public LegModel GetLegModel(int legNumber)
        {           
                using(var datacontext = _contextFactory.CreateDbContext())
                {
                    return datacontext.Legs.Where(l => l.Number == legNumber).FirstOrDefault();
                }         
        }

  


        public async Task AddFlightAsync(Flight flight)
        {
       
            using (var dataContext = _contextFactory.CreateDbContext())
            {
                dataContext.Flights.Add(flight);
                await dataContext.SaveChangesAsync();
            }          
        }

        public Task RemoveFlight(Flight flight)
        {
            throw new NotImplementedException();
        }

        public async void AddLog(Logger log)
        {
            using(var dataContext = _contextFactory.CreateDbContext())
            {
                await dataContext.Logs.AddAsync(log);
                await dataContext.SaveChangesAsync();
            }
            await _hubContext.Clients.All.SendAsync("LogAdded");
        }


        public async void UpdateLegs(IEnumerable<LegModel> legModels)
        {           
                using (var context = _contextFactory.CreateDbContext())
                {
                    context.Legs.UpdateRange(legModels);
                    await context.SaveChangesAsync(); //exception here
                }         
        }

        public async Task<IEnumerable<LegModel>> GetLegModels()
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                return await context.Legs.ToListAsync();
            }

        }

        public async Task<IEnumerable<Logger>> GetLogs()
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                return await context.Logs.ToListAsync();
            }

        }

        public async Task<IEnumerable<Logger>> GetLog(int id)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                return await context.Logs.Where(l =>  l.Id == id).ToListAsync();
            }
        }


        public Task<Flight> GetFlightByCodeAsync(string code)
        {
            throw new NotImplementedException();
        }
    }
}
