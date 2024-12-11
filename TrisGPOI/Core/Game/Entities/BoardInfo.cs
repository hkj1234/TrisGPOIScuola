namespace TrisGPOI.Core.Game.Entities
{
    public class BoardInfo
    {
        public string GameType { get; set; }
        public string Player1 { get; set; }
        public string? Player2 { get; set; }
        public string Board { get; set; } // Rappresentazione del tabellone
        public string CurrentPlayer { get; set; }
        public DateTime LastMoveTime { get; set; }
        public char Victory { get; set; }
    }
}
