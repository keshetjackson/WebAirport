using WebTerminalsServer.Repositories;

namespace WebTerminalsServer.Logic.Legs
{
    public class Leg2 : Leg
    {
        public override int TimeToWait => 9;

        public Leg2()
        {
            Number = 2;
            NextLeg = LegFactory<Leg3>.GetInstance();
        }
        public Leg2(IAirPortRepository repo)
        {
            InitLeg(repo);
            NextLeg = LegFactory<Leg3>.GetInstance(repo);
        }
    }
}