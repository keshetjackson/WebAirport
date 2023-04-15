using System.Diagnostics;
using WebTerminalsServer.Models;
using WebTerminalsServer.Repositories;

namespace WebTerminalsServer.Logic
{
    public class Leg
    {
        protected IAirPortRepository _repository;

        public Leg()
        {
            
        }
        public int Number { get; set; }
        public virtual int TimeToWait => 6;

        public Leg? NextLeg { get; set; }
        public LegModel legModel { get; set; }

        public Flight Flight { get; set; }

        public Flight GetFlight()
        {
            return Flight;
        }
        public void SetFlight(Flight? flight)
        {
            
            Flight = flight;
            if(legModel != null)
            {
                var log = new Logger
                {
                    FlightId = flight?.Id,
                    LegId = legModel?.Id,
                    IsEntering = legModel.Flight == null ? true : false
                };

                legModel.Flight = flight;
                _repository.UpdateLeg(legModel);
                _repository.AddLog(log);
            }
            
        }

     

        public virtual void AddFlight(Flight flight)
        {
            SetFlight(flight);
            Console.WriteLine($"flight {Flight.Code} is in {this.GetType().Name}");
            NextTerminal(flight);
        }

        public virtual void NextTerminal(Flight flight)
        {
            Thread.Sleep(TimeToWait * 300);

            if (NextLeg!.GetFlight() == null)
            {
                Debug.Print($"{flight.Id} = {GetType().Name}");
                SetFlight(null);
                NextLeg.AddFlight(flight);
            }
            else
            {
                NextTerminal(flight);
            }
        }

        public void InitLeg(IAirPortRepository repo)
        {
            var typeName = this.GetType().Name;
            int number = int.Parse(typeName.Substring(typeName.Length - 1));
            Number = number;
            _repository = repo;
            legModel = _repository.GetLegModel(Number);
            if (NextLeg != null)
            {
                legModel.NextLeg = _repository.LegToEnum(NextLeg);
            }
        }
    }
}