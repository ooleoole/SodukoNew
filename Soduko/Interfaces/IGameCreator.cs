using System.Collections.Generic;
using Soduko.GameBoard;

namespace Soduko.Interfaces
{
    public interface IGameCreator
    {
        IDictionary<IGameBoard, IGameBoard> GameBoardGameKeysPair { get;}
        void GenerateGame();
    }
}