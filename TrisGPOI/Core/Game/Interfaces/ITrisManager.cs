namespace TrisGPOI.Core.Game.Interfaces
{
    public interface ITrisManager
    {
        string PlayMove(string board, int position, char simbol);
        char CheckWin(string board);
    }
}
