using System.ComponentModel.DataAnnotations;

namespace TrisGPOI.Database.Friend.Entities
{
    public class DBFriendRequest
    {
        [Key]
        public int Id { get; set; }
        public required string SenderEmail { get; set; }
        public required string ReceiverEmail { get; set; }
    }
}
