using Soduko.GameBoard;
using Soduko.Utilitys;

namespace Soduko.Interfaces
{
    public interface IGameBoardTag
    {
        Coordinate Coordinate { get; }
        int? Value { get; }
        GameBoardTag.Region GameBoardRegion { get; }
        
        
    }
}