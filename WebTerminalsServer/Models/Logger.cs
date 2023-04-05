using Microsoft.EntityFrameworkCore;
using WebTerminalsServer.Logic;

namespace WebTerminalsServer.Models
{
    public class Logger
    {
        public int Id { get; set; }
        public int? FlightId { get; set; } // Add the foreign key property for Flight
        public virtual Flight? Flight { get; set; }
        public int? LegId { get; set; } // Add the foreign key property for Leg
        public virtual LegModel? Leg { get; set; }
        public DateTime EventTime { get; set; } = DateTime.Now;
        public bool IsEntering { get; set; }

    }
}
