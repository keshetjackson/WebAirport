using System.Diagnostics;
using WebTerminalsServer.Models;
using WebTerminalsServer.Services;

namespace WebTerminalsServer.Logic
{

    public class Leg
    {
        protected IAirPortRepository service;

        public Leg()
        {
            
        }
        public int Number { get; set; }
        public virtual int TimeToWait => 6;

        public Leg? NextLeg { get; set; }
        public LegModel legModel { get; set; }

        public virtual Flight flight { get; set; }

        public Flight GetFlight()
        {
            return this.flight;
        }
        public void SetFlight(Flight flight)
        {
            this.flight = flight;

            //if (legModel == null)
            //{
            //    legModel = LegFactory<Leg1>.GetInstance(service).legModel;
            //}
            if(legModel != null)
            legModel.Flight = flight;
        }

     

        public virtual void AddFlight(Flight flight)
        {
            SetFlight(flight);
            //service.UpdateLeg(legModel);
            Console.WriteLine($"flight {this.flight.Code} is in {this.GetType().Name}");
            NextTerminal(flight);
        }

        public virtual void NextTerminal(Flight flight)
        {
            Thread.Sleep(TimeToWait * 100);

            if (NextLeg!.GetFlight() == null)
            {
                Debug.Print($"{flight.Id} = {GetType().Name}");
                SetFlight(null);
                //service.UpdateLeg(legModel);
                NextLeg.AddFlight(flight);
            }
            else
            {
                NextTerminal(flight);
            }
        }

        //public IAirPortService GetService()
        //{
        //    return _service;
        //}

        //public void SetService(IAirPortService service)
        //{
        //    if (this._service == null)
        //        _service = service;
        //}

        public void InitLeg(IAirPortRepository service)
        {
            this.service = service;
            var type = this.GetType().ToString();
            int number = int.Parse(type.Substring(type.Length - 1));
            this.legModel = service.GetLegModel(number);
            //if (this.legModel == null)
            //    this.legModel = null;
            //else
            //    this.legModel = this.legModel;

            if (NextLeg != null)
            {
                legModel.NextLeg = service.LegToEnum(NextLeg);
            }
        }
    }

    public class Departures : Leg
    {


        public Departures(/*IAirPortRepository service*/)
        {
            flights = new PriorityQueue<Flight, bool>();
            //SetService(service);
            NextLeg = LegFactory<Leg6>.GetInstance();
            NextLeg = LegFactory<Leg7>.GetInstance();
            this.service = service;
        }
        private readonly PriorityQueue<Flight, bool>? flights;
        private readonly IAirPortRepository service;

        
        public override void NextTerminal(Flight flight)
        {
            NextLeg.AddFlight(flight);
        }

        public override void AddFlight(Flight flight)
        {
            flights.Enqueue(flight, flight.IsCritical);
            ProgressFlight();
        }

        public void ChangeLeg()
        {
            if (NextLeg is Leg6) NextLeg = LegFactory<Leg7>.GetInstance();
            else NextLeg = LegFactory<Leg6>.GetInstance();
        }

        public void ProgressFlight()
        {
            Thread.Sleep(TimeToWait * 100);
            if (flights != null)
            {
                if (NextLeg!.GetFlight() != null)
                {
                    if (NextLeg.GetFlight().IsDeparture)
                    {
                        ProgressFlight();
                    }
                    else
                    {
                        ChangeLeg();
                        if (NextLeg.GetFlight() == null)
                        {
                            NextTerminal(flights.Dequeue());
                        }
                        else
                        {
                            ProgressFlight();
                        }
                    }
                }
                else
                {
                    ChangeLeg();
                    if (NextLeg.GetFlight() != null)
                    {
                        if (NextLeg.GetFlight().IsDeparture)
                        {
                            ProgressFlight();
                        }
                        else
                        {
                            ChangeLeg();
                            NextTerminal(flights.Dequeue());
                        }
                    }
                    else
                    {
                        NextTerminal(flights.Dequeue());
                    }
                }
            }
        }

    }

    public class Arrivals : Leg
    {
        //public static Arrivals Init { get; } = new Arrivals();

        public Arrivals(/*IAirPortRepository service*/)
        {
            flights = new PriorityQueue<Flight, bool>();
            //SetService(service);
            NextLeg = LegFactory<Leg1>.GetInstance();
            //InitDb(dataContext);
        }

        private readonly PriorityQueue<Flight, bool> flights;

        public override void AddFlight(Flight flight)
        {
            flights.Enqueue(flight, flight.IsCritical);

            if (NextLeg!.GetFlight() == null)
                if(flights.Count > 0)
                    NextTerminal(flights.Dequeue());
        }
    }

    public abstract class MixedLeg : Leg
    {
        public MixedLeg(IAirPortRepository service)
        {
            this.service = service;
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
            Thread.Sleep(TimeToWait * 100);

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

    public class Leg1 : Leg
    {
        public override int TimeToWait => 8;

        public Leg1()
        {
            Number = 1;
            NextLeg = LegFactory<Leg2>.GetInstance();
        }

        public Leg1(IAirPortRepository service)
        {
            InitLeg(service);
            NextLeg = LegFactory<Leg2>.GetInstance(service);
        }
    }

    public class Leg2 : Leg
    {
        public override int TimeToWait => 9;

        public Leg2()
        {
            Number = 2;
            NextLeg = LegFactory<Leg3>.GetInstance();
        }
        public Leg2(IAirPortRepository service)
        {
            InitLeg(service);
            NextLeg = LegFactory<Leg3>.GetInstance(service);
        }
    }

    public class Leg3 : Leg
    {
        public override int TimeToWait => 12;
        public Leg3()
        {
            Number = 3;
            NextLeg = LegFactory<Leg4>.GetInstance();
        }

        public Leg3(IAirPortRepository service)
        {
            InitLeg(service);
            NextLeg = LegFactory<Leg4>.GetInstance(service);
        }
    }

    public class Leg4 : Leg
    {
        static bool isExists;

        public override int TimeToWait => 15;
        public Leg4()
        {
            Number = 4;
        }
        public Leg4(IAirPortRepository service)
        {
            //NextLeg = LegFactory<Leg5>.GetInstance(service);
            if (!isExists)
            {
                isExists = true;
                InitLeg(service);
            }
            this.service = service;
        }
        public override void AddFlight(Flight flight)
        {
            this.SetFlight(flight);
            Console.WriteLine($"flight {GetFlight().Code} is in {this.GetType().Name}");
            if (flight.IsDeparture)
            {
                NextLeg = LegFactory<Leg9>.GetInstance(/*service*/);
                //service.UpdateLeg(legModel);
            }
            else
            {
                NextLeg = LegFactory<Leg5>.GetInstance(/*service*/); 
                //service.UpdateLeg(legModel);
            }
            NextTerminal(flight);
        }
    }

    public class Leg5 : Leg
    {
        public override int TimeToWait => 8;
        public Leg5()
        {
            Number = 5;
            NextLeg = LegFactory<Leg6>.GetInstance();
        }
        public Leg5(IAirPortRepository service)
        {
            InitLeg(service);
            NextLeg = LegFactory<Leg6>.GetInstance(service);
            this.service = service;
        }

        public void ChangeLeg()
        {
            if (this.NextLeg.GetType() == typeof(Leg6)) NextLeg = LegFactory<Leg7>.GetInstance(service);
            else NextLeg = LegFactory<Leg6>.GetInstance(service);
        }

        public override void AddFlight(Flight flight)
        {
            SetFlight(flight);
            Console.WriteLine($"flight  {GetFlight().Code} is in {this.GetType().Name}");
            NextTerminal(flight);
        }

        public override void NextTerminal(Flight flight)
        {
            Thread.Sleep(TimeToWait * 100);

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

    public class Leg7 : MixedLeg
    {
        public Leg7()
        {
            Number = 7;
            NextLeg = LegFactory<Leg8>.GetInstance();
        }
        public override int TimeToWait => 15;


        public Leg7(IAirPortRepository service): base(service)
        {
            InitLeg(service);
            NextLeg = LegFactory<Leg8>.GetInstance(service);
        }
    }

    public class Leg8 : Leg
    {
        public Leg8()
        {
            Number = 8;
            NextLeg = LegFactory<Leg4>.GetInstance();
        }
        public override int TimeToWait => 25;


        public Leg8(IAirPortRepository service)
        {
            InitLeg(service);
            NextLeg = LegFactory<Leg4>.GetInstance(service);
        }

    }

    public class Leg9 : Leg
    {
        public Leg9()
        {
            Number = 9;
        }
        public override int TimeToWait => 20;

        public Leg9(IAirPortRepository service)
        {
            InitLeg(service);
        }
        public override void AddFlight(Flight flight)
        {
            SetFlight(flight);
            //service.UpdateLeg(legModel);
            Console.WriteLine($"flight {GetFlight().Code}  is in  {this.GetType().Name} and ascending");
            Thread.Sleep(TimeToWait * 100);
            NextLeg = null;
            SetFlight(null);
            //service.UpdateLeg(legModel);
        }

    }
}