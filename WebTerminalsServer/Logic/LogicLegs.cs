using System.Diagnostics;
using WebTerminalsServer.Models;
using WebTerminalsServer.Repositories;

namespace WebTerminalsServer.Logic
{

    public class Leg
    {
        protected IAirPortRepository _repository;

        public Leg()
        {
            
        }
        public int Number { get; set; }
        public virtual int TimeToWait => 6;

        public Leg? NextLeg { get; set; }
        public LegModel legModel { get; set; }

        public Flight Flight { get; set; }

        public Flight GetFlight()
        {
            return Flight;
        }
        public void SetFlight(Flight? flight)
        {
            var log = new Logger
            {
                FlightId = flight?.Id,
                LegId = legModel?.Id,
                IsEntering = legModel.Flight == null ? true : false
            };
            Flight = flight;
            if(legModel != null)
            {
                legModel.Flight = flight;
                _repository.UpdateLeg(legModel);
                _repository.AddLog(log);
            }
            
        }

     

        public virtual void AddFlight(Flight flight)
        {
            SetFlight(flight);
            //service.UpdateLeg(legModel);
            Console.WriteLine($"flight {Flight.Code} is in {this.GetType().Name}");
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

        public void InitLeg(IAirPortRepository repo)
        {
            var typeName = this.GetType().Name;
            int number = int.Parse(typeName.Substring(typeName.Length - 1));
            Number = number;
            _repository = repo;
            legModel = _repository.GetLegModel(Number);
            if (NextLeg != null)
            {
                legModel.NextLeg = _repository.LegToEnum(NextLeg);
            }
        }
    }

    public class Departures : Leg
    {
        public Departures(IAirPortRepository repo)
        {
            flights = new PriorityQueue<Flight, bool>();
            //SetService(service);
            NextLeg = LegFactory<Leg6>.GetInstance(repo);
            NextLeg = LegFactory<Leg7>.GetInstance(repo);
            //this._repository = _repository;
        }

        public Departures()
        {
            flights = new PriorityQueue<Flight, bool>();
            NextLeg = LegFactory<Leg6>.GetInstance();
            NextLeg = LegFactory<Leg7>.GetInstance();
            //this._repository = _repository;
        }
        private readonly PriorityQueue<Flight, bool>? flights;
        //private readonly IAirPortRepository _repository;

        
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
        public Arrivals(IAirPortRepository repo)
        {
            flights = new PriorityQueue<Flight, bool>();
            NextLeg = LegFactory<Leg1>.GetInstance(repo);
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
        public MixedLeg(IAirPortRepository repo)
        {
            _repository = repo;
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

        public Leg1(IAirPortRepository repo)
        {
            InitLeg(repo);
            NextLeg = LegFactory<Leg2>.GetInstance(repo);
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
        public Leg2(IAirPortRepository repo)
        {
            InitLeg(repo);
            NextLeg = LegFactory<Leg3>.GetInstance(repo);
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

        public Leg3(IAirPortRepository repo)
        {
            InitLeg(repo);
            NextLeg = LegFactory<Leg4>.GetInstance(repo);
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
            this.SetFlight(flight);
            Console.WriteLine($"flight {GetFlight().Code} is in {this.GetType().Name}");
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
            if (this.NextLeg.GetType() == typeof(Leg6)) NextLeg = LegFactory<Leg7>.GetInstance(_repository);
            else NextLeg = LegFactory<Leg6>.GetInstance(_repository);
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


        public Leg7(IAirPortRepository repo)
        {
            InitLeg(repo);
            NextLeg = LegFactory<Leg8>.GetInstance(repo);
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

        public Leg9(IAirPortRepository repo)
        {
            InitLeg(repo);
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