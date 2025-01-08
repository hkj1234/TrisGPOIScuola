namespace TrisGPOI.Controllers.Tris.Entities
{
    public class GameStatus 
    {
        public int Id { get; set; }
        public string GameType { get; set; }
        public string Player1 { get; set; }
        public bool Player1Online { get; set; }
        public string? Player2 { get; set; }
        public bool Player2Online { get; set; }
        public string Board { get; set; } // Rappresentazione del tabellone
        public string CurrentPlayer { get; set; }
        public DateTime LastMoveTime { get; set; }
        public bool IsFinished { get; set; }
        public char Winning { get; set; }
    }
}
