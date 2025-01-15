using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TrisGPOI.Database.User.Entities;

namespace TrisGPOI.Database.Collection.Entities
{
    public class DBCollectionInventory
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey(nameof(DBUser))]
        public string Email { get; set; }
        //da fare
        //[ForeignKey(nameof(DBCollection))]
        public int CollectionID { get; set; }
        public int Quantity { get; set; }
    }
}
