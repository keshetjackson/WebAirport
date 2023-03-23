using Microsoft.EntityFrameworkCore;
using WebTerminalsServer.Logic;
using WebTerminalsServer.Models;

namespace WebTerminalsServer.Dal
{
    public class DataContext : DbContext
    {
        public virtual DbSet<Leg> legs { get; set; }
        public virtual DbSet<Logger> logs { get; set; }
        public virtual DbSet<Flight> flights { get; set; }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }
    }
}