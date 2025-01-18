using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrisGPOI.Database.Collection.Entities
{
    public class DBCollection
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [ForeignKey(nameof(DBRarity))]
        public int RarityID { get; set; }
    }
}
