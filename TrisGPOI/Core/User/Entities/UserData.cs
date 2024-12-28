namespace TrisGPOI.Core.User.Entities
{
    public class UserData
    {
        public required string Email { get; set; }
        public required string Username { get; set; }
        public int VictoryNormal { get; set; } = 0;
        public int GameNormal { get; set; } = 0;
        public int VictoryInfinity { get; set; } = 0;
        public int GameInfinity { get; set; } = 0;
        public int VictoryUltimate { get; set; } = 0;
        public int GameUltimate { get; set; } = 0;
    }
}
