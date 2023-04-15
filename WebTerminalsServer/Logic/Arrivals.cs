using WebTerminalsServer.Logic.Legs;
using WebTerminalsServer.Models;
using WebTerminalsServer.Repositories;

namespace WebTerminalsServer.Logic
{
    public class Arrivals : Leg
    {

      
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
}