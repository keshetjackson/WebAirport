using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using WebTerminalsServer.Logic;

namespace WebTerminalsServer.Models
{
    public class Flight
    {
        public int Id { get; set; }
        public string? Code { get; set; }
        public bool IsDeparture { get; set; }
        public string Company { get; set; }
        public bool IsCritical { get; set; }

    }
}