using Microsoft.EntityFrameworkCore;
using WebTerminalsServer.Logic;

namespace WebTerminalsServer.Models
{
    public class Logger
    {
        public int Id { get; set; }
        public virtual Flight? Flight { get; set; }
        public virtual LegModel? Leg { get; set; }
        public DateTime In { get; set; } = DateTime.Now;
        public DateTime Out { get; set; }

    }
}
