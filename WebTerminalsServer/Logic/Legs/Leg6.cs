using WebTerminalsServer.Repositories;

namespace WebTerminalsServer.Logic.Legs
{
    public class Leg6 : MixedLeg
    {
        public Leg6()
        {
            Number = 6;
            NextLeg = LegFactory<Leg8>.GetInstance();
        }
        public override int TimeToWait => 15;


        public Leg6(IAirPortRepository service) : base(service)
        {
            InitLeg(service);
            NextLeg = LegFactory<Leg8>.GetInstance(service);
        }
    }
}