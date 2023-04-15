using WebTerminalsServer.Logic.Legs;
using WebTerminalsServer.Models;
using WebTerminalsServer.Repositories;

namespace WebTerminalsServer.Logic
{
    public class Departures : Leg
    {
        public Departures(IAirPortRepository repo)
        {
            flights = new PriorityQueue<Flight, bool>();
            NextLeg = LegFactory<Leg6>.GetInstance(repo);
            NextLeg = LegFactory<Leg7>.GetInstance(repo);
        }

        private readonly PriorityQueue<Flight, bool>? flights;

        
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
}