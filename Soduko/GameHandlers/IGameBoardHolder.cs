using Soduko.GameBoard;

namespace Soduko.GameHandlers
{
    public interface IGameBoardHolder
    {
        IGameBoard GameBoard { get;}
    }
}