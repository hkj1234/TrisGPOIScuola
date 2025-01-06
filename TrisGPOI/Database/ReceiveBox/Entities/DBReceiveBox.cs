using System.ComponentModel.DataAnnotations;

namespace TrisGPOI.Core.ReceiveBox.Entities
{
    public class DBReceiveBox
    {
        [Key]
        public int Id { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public DateTime ExpireDate { get; set; }
        public bool IsRead { get; set; }
    }
}
