using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TrisGPOI.Database.User.Entities;

namespace TrisGPOI.Database.Friend.Entities
{
    public class DBFriendRequest
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(DBUser))]
        public required string SenderEmail { get; set; }
        [ForeignKey(nameof(DBUser))]
        public required string ReceiverEmail { get; set; }
    }
}
