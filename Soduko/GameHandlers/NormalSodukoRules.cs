using System.Linq;
using Soduko.GameBoard;
using Soduko.Interfaces;

namespace Soduko.GameHandlers
{
    public class NormalSodukoRules : IGameBoardRules
    {

        public bool ValidateGameBoardTag(IGameBoardTag tag, IGameBoard gameBoard, IGameBoard startingBase)
        {
            if (startingBase.Any(t => t.Coordinate == tag.Coordinate && t.Value != null))
            {
                return false;
            }
            
            return !gameBoard.Any(t => (t.Coordinate.X == tag.Coordinate.X && t.Value == tag.Value) ||
                                                            (t.Coordinate.Y == tag.Coordinate.Y && t.Value == tag.Value) ||
                                                            t.GameBoardRegion == tag.GameBoardRegion
                                                            && t.Value == tag.Value);


        }
    }
}