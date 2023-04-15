using WebTerminalsServer.Repositories;

namespace WebTerminalsServer.Logic.Legs
{
    public class Leg1 : Leg
    {
        public override int TimeToWait => 8;

        public Leg1()
        {
            Number = 1;
            NextLeg = LegFactory<Leg2>.GetInstance();
        }

        public Leg1(IAirPortRepository repo)
        {
            InitLeg(repo);
            NextLeg = LegFactory<Leg2>.GetInstance(repo);
        }
    }
}