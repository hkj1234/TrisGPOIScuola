using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrisGPOI.Database.User.Entities
{
    public class DBUserVittoriePVP
    {
        [Key]
        [ForeignKey(nameof(DBUser))]
        public string Email { get; set; }


        public int VictoryNormal { get; set; } = 0;
        public int LossesNormal { get; set; } = 0;
        public int GameNormal { get; set; } = 0;


        public int VictoryInfinity { get; set; } = 0;
        public int LossesInfinity { get; set; } = 0;
        public int GameInfinity { get; set; } = 0;


        public int VictoryUltimate { get; set; } = 0;
        public int LossesUltimate { get; set; } = 0;
        public int GameUltimate { get; set; } = 0;
    }
}
