using System.ComponentModel.DataAnnotations;

namespace TrisGPOI.Database.Game.Entities
{
    public class DBGameInvite
    {
        [Key]
        public int Id { get; set; }
        public string InviterEmail { get; set; }
        public string InvitedEmail { get; set; }
        public string GameType { get; set; }
        public DateTime Date { get; set; }
    }
}
