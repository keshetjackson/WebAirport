namespace WebTerminalsServer.Models
{
    public class LegModel
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public virtual Flight? Flight { get; set; }
        public LegType NextLeg { get; set; }
    }

    [Flags]
    public enum LegType : ushort
    {
        None = 0,
        One = 1,
        Two = 2,
        Three = 4,
        Four = 8,
        Five = 16,
        Six = 32,
        Seven = 64,
        Eight = 128,
        Nine = 256
    }
}
