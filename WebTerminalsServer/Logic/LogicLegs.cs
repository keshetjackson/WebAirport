using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using WebTerminalsServer.Dal;
using WebTerminalsServer.Models;
using WebTerminalsServer.Services;

namespace WebTerminalsServer.Logic
{
    public class LegFactory<T> where T : Leg
    {
        private static readonly Dictionary<string, T> instances = new Dictionary<string, T>();

        private LegFactory() { }

        public static T GetInstance(IAirPortService service)
        {
            var typeName = typeof(T).Name;
            if (!instances.ContainsKey(typeName))
            {
                lock (instances)
                {
                    if (!instances.ContainsKey(typeName))
                    {
                        var instance = (T)Activator.CreateInstance(typeof(T), service);
                        instances.Add(typeName, instance);
                    }
                }
            }
            return instances[typeName];
        }
    }
    public class Leg
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [NotMapped]
        //public DataContext _dbContext { get; set; }
        private IAirPortService _service;
        public virtual int TimeToWait => 6;
        
        public Leg? NextLeg { get; set; }

        public virtual Flight flight { get; set; }

        public Flight GetFlight()
        {
            return this.flight;
        }
        public void SetFlight(Flight flight)
        {
            flight = flight;
        }

        public virtual void AddFlight(Flight flight)
        {
            flight = flight;
            Console.WriteLine($"flight {this.flight.Code} is in {this.GetType().Name}");
            NextTerminal(flight);
        }

        public virtual void NextTerminal(Flight flight)
        {
            Thread.Sleep(TimeToWait * 100);

            if (NextLeg!.GetFlight() == null)
            {
                Debug.Print($"{flight.Id} = {GetType().Name}");
                NextLeg.AddFlight(flight);
                flight = null;
            }
            else
            {                
                NextTerminal(flight);
            }
        }

        public IAirPortService GetService()
        {
            return _service;
        }

        public void SetService(IAirPortService service)
        {
            if(this._service == null)
            _service = service;
        }

        //public void InitDb(DataContext dataContext)
        //{
        //    object obj = new object();
        //    if(_dbContext == null)
        //    {
        //        var type = GetType().ToString();
        //        Id = int.Parse(type.Substring(type.Length - 1));
        //        _dbContext = dataContext;
        //        _dbContext.legs.Add(this);
        //    }
        //    if(NextLeg != null)
        //    {
        //        if(NextLeg._dbContext == null)
        //        NextLeg.InitDb(dataContext);
        //    }
        //    lock(obj)
        //    {
        //        _dbContext.SaveChangesAsync().Wait();
        //    }
        //}
    }

    public class Departures : Leg 
    {
        
        //public static Departures Init { get; } = new Departures();
        public Departures(IAirPortService service)
        {
            SetService(service);
            NextLeg = LegFactory<Leg6>.GetInstance(service);
            //InitDb(dataContext);
            NextLeg = LegFactory<Leg7>.GetInstance(service);
        }
        static readonly PriorityQueue<Flight, bool> flights = new PriorityQueue<Flight, bool>();
        public void ChangeLeg()
        {
            if (this.NextLeg.GetType() == typeof(Leg6)) NextLeg = LegFactory<Leg7>.GetInstance(GetService());
            else NextLeg = LegFactory<Leg6>.GetInstance(GetService());
        }
        public override void NextTerminal(Flight flight)
        {
            NextLeg.AddFlight(flight);
        }

        public override void AddFlight(Flight flight)
        {
            flights.Enqueue(flight, flight.IsCritical);
            ProgressFlight();                
        }

        public void ProgressFlight()
        {
            Thread.Sleep(TimeToWait * 100);
            if(flights != null)
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
        public Arrivals(IAirPortService service)
        {
            SetService(service);
            NextLeg = LegFactory<Leg1>.GetInstance(service);
            //InitDb(dataContext);
        }

        static readonly PriorityQueue<Flight,bool> flights = new PriorityQueue<Flight,bool>();

        public override void AddFlight(Flight flight)
        {
            flights.Enqueue(flight,flight.IsCritical);

            if (NextLeg!.GetFlight() == null)
                NextTerminal(flights.Dequeue());
        }
    }

    public abstract class MixedLeg : Leg
    {
        public override void AddFlight(Flight flight)
        {
            SetFlight(flight);
            Console.WriteLine($"flight  {GetFlight().Code} is in {this.GetType().Name}");
            if (flight.IsDeparture)
            {
                NextLeg = LegFactory<Leg8>.GetInstance(GetService());
            }
            else NextLeg = null;
            NextTerminal(flight);
        }

        public override void NextTerminal(Flight flight)
        {
            Thread.Sleep(TimeToWait * 100 );
            if (NextLeg != null)
            {
                if (NextLeg!.GetFlight() == null)
                {
                    Debug.Print($"{flight.Id} = {GetType().Name}");
                    NextLeg.AddFlight(flight);
                    SetFlight(null);
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
            }
        }

    }

    public class Leg1 : Leg
    {
        public override int TimeToWait => 8;
        //#region Singelton SIMPLE
        //public static Leg1 Init { get; } = new Leg1();
        //#endregion
        public Leg1(IAirPortService service)
        {
            SetService(service);
            NextLeg = LegFactory<Leg2>.GetInstance(service);
        }
    }

    public class Leg2 : Leg
    {
        public override int TimeToWait => 9;
        //#region Singleton Less Code
        //private static Leg2 init;
        //public static Leg2 Init
        //{
        //    get
        //    {
        //        init ??= new Leg2();
        //        return init;
        //    }
        //}
        //#endregion
        //public static Leg2 Init { get; } = new Leg2();

        public Leg2(IAirPortService service)
        {
            SetService(service);
            NextLeg = LegFactory<Leg3>.GetInstance(service);
        }
    }

    public class Leg3 : Leg
    {
        public override int TimeToWait => 12;
        //public static Leg3 Init { get; } = new Leg3();

        public Leg3(IAirPortService service)
        {
            SetService(service);
            NextLeg = LegFactory<Leg4>.GetInstance(service);
        }
    }

    public class Leg4 : Leg
    {
        public override int TimeToWait => 15;
        //threadsafe singletone
        //private static readonly Lazy<Leg4> lazy = new Lazy<Leg4>(() => new Leg4());
        //public static Leg4 Init = lazy.Value;

        public Leg4(IAirPortService service)
        {
           SetService(service);
        }
        public override void AddFlight(Flight flight)
        {
            this.SetFlight(flight);
            Console.WriteLine($"flight {GetFlight().Code} is in {this.GetType().Name}");
            if (flight.IsDeparture)
            {
                NextLeg = LegFactory<Leg9>.GetInstance(GetService());
                //if (NextLeg._dbContext == null) NextLeg._dbContext = _dbContext;
            }
            else
            {
                NextLeg = NextLeg = LegFactory<Leg5>.GetInstance(GetService()); ;
                //if(NextLeg._dbContext == null) NextLeg._dbContext = _dbContext;
            }
            NextTerminal(flight);
        }
    }

    public class Leg5 : Leg
    {
        public override int TimeToWait => 8;
        //#region Singelton SIMPLE
        //public static Leg5 Init { get; } = new Leg5();
        //#endregion
        public Leg5(IAirPortService service)
        {
            SetService(service);
            NextLeg = LegFactory<Leg6>.GetInstance(service);
            //if (NextLeg._dbContext == null) NextLeg._dbContext = _dbContext;
        }

        public void ChangeLeg()
        {
            if (this.NextLeg.GetType() == typeof(Leg6)) NextLeg = LegFactory<Leg7>.GetInstance(GetService());
            else NextLeg = LegFactory<Leg6>.GetInstance(GetService());
        }
        public override void AddFlight(Flight flight)
        {
            this.SetFlight(flight);
            Console.WriteLine($"flight  {GetFlight().Code} is in {this.GetType().Name}");
            NextTerminal(flight);
        }

        public override void NextTerminal(Flight flight)
        {
            if(NextLeg.GetFlight() != null)
            {
                ChangeLeg();
            }

            Thread.Sleep(TimeToWait * 100 );

            if (NextLeg!.GetFlight() == null)
            {
                Debug.Print($"{flight.Id} = {GetType().Name}");
                NextLeg.AddFlight(flight);
                SetFlight(null);
            }
            else
            {
                NextTerminal(flight);
            }
        }
    }

    public class Leg6 : MixedLeg
    {
        public override int TimeToWait => 15;
        //threadsafe singletone
        //private static readonly Lazy<Leg6> lazy = new Lazy<Leg6>(() => new Leg6());
        //public static Leg6 Init = lazy.Value;

        public Leg6(IAirPortService service)
        {
            SetService(service);
            NextLeg = LegFactory<Leg8>.GetInstance(service);
        }
    }

    public class Leg7 : MixedLeg

    {
        public override int TimeToWait => 15;
        //threadsafe singletone
        //private static readonly Lazy<Leg7> lazy = new Lazy<Leg7>(() => new Leg7());
        //public static Leg7 Init = lazy.Value;

        public Leg7(IAirPortService service)
        {
            SetService(service);
            NextLeg = LegFactory<Leg8>.GetInstance(service);
        }
    }

    public class Leg8 : Leg
    {
        public override int TimeToWait => 25;
        //threadsafe singletone
        //private static readonly Lazy<Leg8> lazy = new Lazy<Leg8>(() => new Leg8());
        //public static Leg8 Init = lazy.Value;

        public Leg8(IAirPortService service)
        {
            SetService(service);
            NextLeg = LegFactory<Leg4>.GetInstance(service);
        }
        public virtual void AddFlight(Flight flight)
        {
            SetFlight(flight);
            Console.WriteLine($"flight {GetFlight().Code} is in {this.GetType().Name}");
            if (NextLeg.GetType() != typeof(Leg4)) NextLeg = LegFactory<Leg4>.GetInstance(GetService());
            NextTerminal(flight);
        }

    }

    public class Leg9 : Leg
    {
        public override int TimeToWait => 20;
        //#region Singleton simple
        //public static Leg9 Init { get; } = new Leg9();
        //#endregion
        public Leg9(IAirPortService service)
        {
            SetService(service);
        }
        public override void AddFlight(Flight flight)
        {
            SetFlight(flight);
            Console.WriteLine($"flight {GetFlight().Code}  is in  {this.GetType().Name} and ascending");
            Thread.Sleep(TimeToWait * 100);
            NextLeg = null;
            SetFlight(null);
        }

    }
}