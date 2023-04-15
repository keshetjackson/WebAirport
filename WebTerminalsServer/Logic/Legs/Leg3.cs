using WebTerminalsServer.Repositories;

namespace WebTerminalsServer.Logic.Legs
{
    public class Leg3 : Leg
    {
        public override int TimeToWait => 12;
        public Leg3()
        {
            Number = 3;
            NextLeg = LegFactory<Leg4>.GetInstance();
        }

        public Leg3(IAirPortRepository repo)
        {
            InitLeg(repo);
            NextLeg = LegFactory<Leg4>.GetInstance(repo);
        }
    }
}