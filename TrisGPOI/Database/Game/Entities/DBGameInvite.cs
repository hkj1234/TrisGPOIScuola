using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TrisGPOI.Database.User.Entities;

namespace TrisGPOI.Database.Game.Entities
{
    public class DBGameInvite
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(DBUser))]
        public string InviterEmail { get; set; }
        [ForeignKey(nameof(DBUser))]
        public string InvitedEmail { get; set; }
        public string GameType { get; set; }
        public DateTime Date { get; set; }
    }
}
