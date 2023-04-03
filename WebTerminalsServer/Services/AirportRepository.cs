using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using WebTerminalsServer.Dal;
using WebTerminalsServer.Logic;
using WebTerminalsServer.Models;

namespace WebTerminalsServer.Services
{
    public class AirportRepository : IAirPortRepository
    {
        private readonly DataContext _dataContext;

        static object obj;
        public AirportRepository(DataContext dataContext)
        {
            if (obj == null)
                obj = new object();
            _dataContext = dataContext;
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

        public async void UpdateLeg(LegModel leg)
        {
            try
            {
                //lock(obj)
                //{
                //    _dataContext.Legs.Update(leg);
                //    _dataContext.SaveChangesAsync();
                //}
                _dataContext.Legs.Update(leg);
                await _dataContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
             
        }

        public LegModel GetLegModel(int legNumber)
        {
            try
            {
                lock (obj)
                    return _dataContext.Legs.Where(l => l.Number == legNumber).FirstOrDefault();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public Task SaveAsync()
        {
            throw new NotImplementedException();
        }

        public Task AddLegAsync(LegModel leg)
        {
            throw new NotImplementedException();
        }

        public Task RemoveLeg(LegModel leg)
        {
            throw new NotImplementedException();
        }



        public Task AddFlightAsync(Flight flight)
        {
            throw new NotImplementedException();
        }

        public Task RemoveFlight(Flight flight)
        {
            throw new NotImplementedException();
        }

        public Task UpdateFlight(Flight flight)
        {
            throw new NotImplementedException();
        }

        public Task AddLogAsync(Logger log)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LegModel> GetLegModels()
        {
            return _dataContext.Legs!.ToList();
        }

        public async void UpdateLegs(IEnumerable<LegModel> legModels)
        {
            try
            {
                //lock (obj)
                //{
                //    _dataContext.Legs.UpdateRange(legModels);
                //    _dataContext.SaveChangesAsync();
                //}
                _dataContext.Legs.UpdateRange(legModels);
                await _dataContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

        public async Task<IEnumerable<LegModel>> AsyncGetLegModels()
        {
            return await _dataContext.Legs.ToListAsync();
        }
    }
}
