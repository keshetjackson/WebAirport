using WebTerminalsServer.Repositories;

namespace WebTerminalsServer.Logic.Legs
{
    public class Leg8 : Leg
    {
        public Leg8()
        {
            Number = 8;
            NextLeg = LegFactory<Leg4>.GetInstance();
        }
        public override int TimeToWait => 25;


        public Leg8(IAirPortRepository repo)
        {
            InitLeg(repo);
            NextLeg = LegFactory<Leg4>.GetInstance(repo);
        }

    }
}