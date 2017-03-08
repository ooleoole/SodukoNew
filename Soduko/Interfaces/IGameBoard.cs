using System.Collections.Generic;
using Soduko.Utilitys;

namespace Soduko.GameBoard
{
    public interface IGameBoard : IEnumerable<GameBoardTag>
    {
        int Count { get; }
        int GameBoardRoot { get; }
        int GameBoardSize { get; }
        IEnumerator<GameBoardTag> GetEnumerator();
        void Add(GameBoardTag tag);
        void Clear();
        void Replace(GameBoardTag tag);
        bool RemoveAt(Coordinate coordinate);
        bool Remove(GameBoardTag tag);
        IGameBoard Clone();
    }
}