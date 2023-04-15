using System.Diagnostics;
using WebTerminalsServer.Models;
using WebTerminalsServer.Repositories;

namespace WebTerminalsServer.Logic.Legs
{
    public class Leg5 : Leg
    {
        public override int TimeToWait => 8;
        public Leg5()
        {
            Number = 5;
            NextLeg = LegFactory<Leg6>.GetInstance();
        }
        public Leg5(IAirPortRepository repo)
        {
            InitLeg(repo);
            NextLeg = LegFactory<Leg6>.GetInstance(repo);
        }

        public void ChangeLeg()
        {
            if (NextLeg.GetType() == typeof(Leg6)) NextLeg = LegFactory<Leg7>.GetInstance(_repository);
            else NextLeg = LegFactory<Leg6>.GetInstance(_repository);
        }

        public override void AddFlight(Flight flight)
        {
            SetFlight(flight);
            Console.WriteLine($"flight  {GetFlight().Code} is in {GetType().Name}");
            NextTerminal(flight);
        }

        public override void NextTerminal(Flight flight)
        {
            Thread.Sleep(TimeToWait * 300);

            if (NextLeg.GetFlight() != null)
            {
                ChangeLeg();
            }


            if (NextLeg!.GetFlight() == null)
            {
                Debug.Print($"{flight.Id} = {GetType().Name}");
                SetFlight(null);
                NextLeg.AddFlight(flight);
                //service.UpdateLeg(legModel);
            }
            else
            {
                NextTerminal(flight);
            }
        }
    }
}