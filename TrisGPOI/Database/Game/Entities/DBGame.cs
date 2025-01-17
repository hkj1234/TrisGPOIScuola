using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace TrisGPOI.Database.Game.Entities
{
    public class DBGame
    {
        [Key]
        public int Id { get; set; }
        public string GameType {  get; set; }
        public string Player1 { get; set; }
        [AllowNull]
        public string? Player2 { get; set; }
        public string Board { get; set; } // Rappresentazione del tabellone
        public string CurrentPlayer { get; set; }
        public DateTime LastMoveTime { get; set; }
        public bool IsFinished { get; set; }
        public char Winning {  get; set; }
    }
}
