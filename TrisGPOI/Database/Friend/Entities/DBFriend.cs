using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TrisGPOI.Database.User.Entities;

namespace TrisGPOI.Database.Friend.Entities
{
    public class DBFriend
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(DBUser))]
        public string User1Email { get; set; }
        public virtual DBUser User1 { get; set; }

        [ForeignKey(nameof(DBUser))]
        public string User2Email { get; set; }
        public virtual DBUser User2 { get; set; }
    }
}
