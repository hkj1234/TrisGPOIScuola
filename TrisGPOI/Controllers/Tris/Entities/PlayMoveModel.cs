namespace TrisGPOI.Controllers.Tris.Entities
{
    public class PlayMoveModel
    {
        public required int position {  get; set; }
    }

    public class PlayOnlineModel
    {
        public required string Mode { get; set; }
    }

    public class PlayWithCPUModel : PlayOnlineModel
    {
        public required string Difficulty { get; set; }
    }
}
