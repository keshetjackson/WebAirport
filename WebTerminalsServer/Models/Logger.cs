using Microsoft.EntityFrameworkCore;
using WebTerminalsServer.Logic;

namespace WebTerminalsServer.Models
{
    public class Logger
    {
        public int Id { get; set; }
        public virtual Flight flight { get; set; }
        public virtual Leg leg { get; set; }
        public DateTime In { get; set; }
        public DateTime Out { get; set; }

        public Logger() { }
        public Logger(Flight newFlight, Leg newLeg)
        {
            flight = newFlight;
            leg = newLeg;
            In = DateTime.Now;
            Out = DateTime.Now;
        }

    }
}
