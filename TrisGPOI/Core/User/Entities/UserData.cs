namespace TrisGPOI.Core.User.Entities
{
    public class UserData
    {
        public required string Email { get; set; }
        public required string Username { get; set; }
        public string Status { get; set; } = "Offline";
        public string FotoProfilo { get; set; } = "Default";
        public string Description { get; set; } = "";
        public int VictoryNormal { get; set; } = 0;
        public int GameNormal { get; set; } = 0;
        public int VictoryInfinity { get; set; } = 0;
        public int GameInfinity { get; set; } = 0;
        public int VictoryUltimate { get; set; } = 0;
        public int GameUltimate { get; set; } = 0;

        public int MoneyXO { get; set; } = 0;
        public int Level { get; set; } = 1;
        public int Experience { get; set; } = 0;
    }
}
