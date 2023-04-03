using Microsoft.EntityFrameworkCore;
using WebTerminalsServer.Logic;
using WebTerminalsServer.Models;

namespace WebTerminalsServer.Dal
{
    public class DataContext : DbContext
    {
        public virtual DbSet<Flight> Flights { get; set; }
        public virtual DbSet<LegModel> Legs { get; set; }
        public virtual DbSet<Logger> Logs { get; set; }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Flight>(entity =>
            {
                entity.HasIndex(e => e.Code).IsUnique();
            });

            modelBuilder.Entity<LegModel>().HasData(
                new LegModel { Id = 1, Number = 1, NextLeg = LegType.Two },
                new LegModel { Id = 2, Number = 2, NextLeg = LegType.Three },
                new LegModel { Id = 3, Number = 3, NextLeg = LegType.Four },
                new LegModel { Id = 4, Number = 4, NextLeg = LegType.Five },
                new LegModel { Id = 5, Number = 5, NextLeg = LegType.Six },
                new LegModel { Id = 6, Number = 6, NextLeg = LegType.Eight },
                new LegModel { Id = 7, Number = 7, NextLeg = LegType.Eight },
                new LegModel { Id = 8, Number = 8, NextLeg = LegType.Four },
                new LegModel { Id = 9, Number = 9, NextLeg = LegType.None }
            );
        }
    }
}