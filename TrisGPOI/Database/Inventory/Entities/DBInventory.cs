using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TrisGPOI.Database.Collection.Entities;
using TrisGPOI.Database.User.Entities;

namespace TrisGPOI.Database.Inventory.Entities
{
    public class DBInventory
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey(nameof(DBUser))]
        public string Email { get; set; }
        [ForeignKey(nameof(DBCollection))]
        public int ItemID { get; set; }
        public int Quantity { get; set; }
    }
}
