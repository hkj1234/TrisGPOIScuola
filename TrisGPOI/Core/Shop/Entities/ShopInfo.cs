namespace TrisGPOI.Core.Shop.Entities
{
    public class ShopInfo
    {
        public string CollectionName { get; set; }
        public int CollectionId { get; set; }
        public int Amount { get; set; }
        public int Price { get; set; }
        public bool IsPurchased { get; set; }
    }
}
