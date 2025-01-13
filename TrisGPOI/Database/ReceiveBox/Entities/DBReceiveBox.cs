using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TrisGPOI.Database.User.Entities;

namespace TrisGPOI.Database.ReceiveBox.Entities
{
    public class DBReceiveBox
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(DBUser))]
        public required string Sender { get; set; }
        [ForeignKey(nameof(DBUser))]
        public string Receiver { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public DateTime ExpireDate { get; set; }
        public bool IsRead { get; set; }
    }
}
