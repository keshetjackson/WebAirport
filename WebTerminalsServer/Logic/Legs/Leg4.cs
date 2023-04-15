using WebTerminalsServer.Models;
using WebTerminalsServer.Repositories;

namespace WebTerminalsServer.Logic.Legs
{
    public class Leg4 : Leg
    {
        static bool isExists;

        public override int TimeToWait => 15;
        public Leg4()
        {
            Number = 4;
        }
        public Leg4(IAirPortRepository repo)
        {
            //NextLeg = LegFactory<Leg5>.GetInstance(service);
            if (!isExists)
            {
                isExists = true;
                InitLeg(repo);
            }
        }
        public override void AddFlight(Flight flight)
        {
            SetFlight(flight);
            Console.WriteLine($"flight {GetFlight().Code} is in {GetType().Name}");
            if (flight.IsDeparture)
            {
                NextLeg = LegFactory<Leg9>.GetInstance(_repository);
                //service.UpdateLeg(legModel);
            }
            else
            {
                NextLeg = LegFactory<Leg5>.GetInstance(_repository);
                //service.UpdateLeg(legModel);
            }
            NextTerminal(flight);
        }
    }
}