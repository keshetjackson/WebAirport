using WebTerminalsServer.Models;
using WebTerminalsServer.Repositories;

namespace WebTerminalsServer.Logic.Legs
{

    public class Leg9 : Leg
    {
        public Leg9()
        {
            Number = 9;
        }
        public override int TimeToWait => 20;

        public Leg9(IAirPortRepository repo)
        {
            InitLeg(repo);
        }
        public override void AddFlight(Flight flight)
        {
            SetFlight(flight);
            //service.UpdateLeg(legModel);
            Console.WriteLine($"flight {GetFlight().Code}  is in  {GetType().Name} and ascending");
            Thread.Sleep(TimeToWait * 100);
            NextLeg = null;
            SetFlight(null);
            //service.UpdateLeg(legModel);
        }

    }
}