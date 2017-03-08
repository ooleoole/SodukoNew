namespace Soduko.GameBoard
{
    public interface IGameBoardTag
    {
        Coordinate Coordinate { get; }
        int? Value { get; }
        GameBoardTag.Region GameBoardRegion { get; }
        string ToString();
        
    }
}