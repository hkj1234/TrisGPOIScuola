using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using TrisGPOI.Database.User.Entities;

namespace TrisGPOI.Database.Game.Entities
{
    public class DBGame
    {
        [Key]
        public int Id { get; set; }
        public string GameType {  get; set; }
        [ForeignKey(nameof(DBUser))]
        public string Player1 { get; set; }
        [ForeignKey(nameof(DBUser))]
        [AllowNull]
        public string? Player2 { get; set; }
        public string Board { get; set; } // Rappresentazione del tabellone
        public string CurrentPlayer { get; set; }
        public DateTime LastMoveTime { get; set; }
        public bool IsFinished { get; set; }
        public char Winning {  get; set; }
    }
}
