namespace TrisGPOI.Core.Game.Interfaces
{
    public interface ITrisManager
    {
        string PlayMove(string board, int position, char simbol);
        char CheckWin(string board);
        bool IsEmptyPosition(string board, int position);
        string CreateEmptyBoard();
        bool IsEmpty(string board);
        List<int> GetValidPosition(string board);
    }
}
