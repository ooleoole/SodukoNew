using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soduko.GameBoard
{
    public class GameBoardOLD : IEnumerable<int?>
    {
        private int?[,] _gameBoard;
        private bool _isReadOnly;
        private int _gameBoardRoot;
        private delegate bool LoopAction(int? item, int x, int y);
        public int Count => _gameBoard.Length;
        public bool IsReadOnly => _isReadOnly;

        
        public GameBoardOLD(int gameBoardRoot)
        {
            _gameBoardRoot = gameBoardRoot;
            _gameBoard = new int?[gameBoardRoot, gameBoardRoot];
        }

        public void Add(int? item)
        {
            LoopAction loopAction = Add;
            if (!GameBoardLooper(item, loopAction))
            {
                throw new ArgumentException("GameBoard Is full");
            }
        }

        private bool Add(int? item, int x, int y)
        {

            if (_gameBoard[x, y] == null)
            {
                _gameBoard[x, y] = item;
                return true;
            }
            return false;
        }

        public void Clear()
        {
            _gameBoard = new int?[_gameBoardRoot, _gameBoardRoot];
        }


        public bool Contains(int? item)
        {
            LoopAction loopAction = Contains;
            return GameBoardLooper(item, loopAction);
        }

        private bool GameBoardLooper(int? item, LoopAction loopAction)
        {
            for (int x = 0; x < _gameBoardRoot; x++)
            {
                for (int y = 0; y < _gameBoardRoot; y++)
                {
                    if (loopAction(item, x, y))
                        return true;
                }
            }
            return false;
        }

        private bool Contains(int? item, int x, int y)
        {
            return _gameBoard[x, y] == item;
        }

        public void CopyTo(int?[] array, int arrayIndex)
        {
            _gameBoard.CopyTo(array, arrayIndex);
        }

        

        public bool Remove(int? item)
        {
            LoopAction loopAction = Remove;
            return GameBoardLooper(item, loopAction);

        }

        private bool Remove(int? item, int x, int y)
        {
            if (_gameBoard[x, y] == item)
            {
                _gameBoard[x, y] = null;
                return true;
            }
            return false;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _gameBoard.GetEnumerator();
        }

        public IEnumerator<int?> GetEnumerator()
        {
            return (IEnumerator<int?>)_gameBoard.GetEnumerator();
        }
    }
}
