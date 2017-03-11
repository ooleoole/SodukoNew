using System.Collections.Generic;
using Soduko.GameBoard;

namespace Soduko.Interfaces
{
    public interface IGameHolder
    {
        IDictionary<IGameBoard, IGameBoard> GameBoardGameKeysPair { get;}
        void LoadGame();
    }
}