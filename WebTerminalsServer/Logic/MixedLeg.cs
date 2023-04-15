using System.Diagnostics;
using WebTerminalsServer.Logic.Legs;
using WebTerminalsServer.Models;
using WebTerminalsServer.Repositories;

namespace WebTerminalsServer.Logic
{
    public abstract class MixedLeg : Leg
    {
        public MixedLeg(IAirPortRepository repo)
        {
            _repository = repo;
        }
        public MixedLeg()
        {
            
        }

        public override void AddFlight(Flight flight)
        {
            SetFlight(flight);
            Console.WriteLine($"flight  {GetFlight().Code} is in {this.GetType().Name}");
            if (flight.IsDeparture)
            {
                NextLeg = LegFactory<Leg8>.GetInstance();

            }
            else NextLeg = null;
            NextTerminal(flight);
        }

        public override void NextTerminal(Flight flight)
        {
            Thread.Sleep(TimeToWait * 300);

            if (NextLeg != null)
            {
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
            else
            {
                Console.WriteLine($"flight {GetFlight().Code} has finished landing");
                SetFlight(null);
                //service.UpdateLeg(legModel);
            }
        }

    }
}