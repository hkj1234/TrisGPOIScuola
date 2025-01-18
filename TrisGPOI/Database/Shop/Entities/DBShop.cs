using System.ComponentModel.DataAnnotations;

namespace TrisGPOI.Database.Shop.Entities
{
    public class DBShop
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public string CollectionId { get; set; }
        public string Amount { get; set; }
        public int Prince { get; set; }
        public bool Purchased { get; set; }
    }
}
