using System.ComponentModel.DataAnnotations;

namespace TrisGPOI.Database.Collection.Entities
{
    public class DBRarity
    {
        [Key]
        public int Id { get; set; }
        [Key]
        public string Name { get; set; }
    }
}
