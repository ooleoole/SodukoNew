using Soduko.GameBoard;
using Soduko.Interfaces;

namespace Soduko.GameHandlers
{
    public interface IGameBoardRules
    {
        bool ValidateGameBoardTag(IGameBoardTag tag, IGameBoard gameBoard, IGameBoard startingBase);
    }
}