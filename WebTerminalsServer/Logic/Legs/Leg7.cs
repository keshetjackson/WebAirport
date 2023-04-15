using WebTerminalsServer.Repositories;

namespace WebTerminalsServer.Logic.Legs
{
    public class Leg7 : MixedLeg
    {
        public Leg7()
        {
            Number = 7;
            NextLeg = LegFactory<Leg8>.GetInstance();
        }
        public override int TimeToWait => 15;


        public Leg7(IAirPortRepository repo)
        {
            InitLeg(repo);
            NextLeg = LegFactory<Leg8>.GetInstance(repo);
        }
    }
}