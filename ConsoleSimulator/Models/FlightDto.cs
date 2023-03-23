namespace ConsoleSimulator.Models
{
    //DTO = Data Transfer Object
    public class FlightDto
    {
        public string? Code { get; set; }
        public FlightDto() => Code = Guid.NewGuid().ToString("N").Substring(0,6);
        public bool IsDeparture { get; set; }

        public string Company { get; set; }
        public bool IsCritical { get; set; }
    }
}