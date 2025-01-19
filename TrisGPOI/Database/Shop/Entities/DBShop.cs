using System.ComponentModel.DataAnnotations;

namespace TrisGPOI.Database.Shop.Entities
{
    public class DBShop
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public int CollectionId { get; set; }
        public int Amount { get; set; }
        public int Price { get; set; }
        public bool Purchased { get; set; }
    }
}

