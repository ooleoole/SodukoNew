using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Soduko.GameBoard;

namespace Soduko.GameHandlers
{
    public class GameHandler
    {
        private GameBoard2 _gameBoard;
        private int _difficultyLevel;
        private int GameBoardRoot => _gameBoard.GameBoardRoot;
        public GameHandler(GameBoard2 gameBoard, int difficultylevel)
        {
            _gameBoard = gameBoard;
            _difficultyLevel = difficultylevel;
        }

        private GameBoardTag GetRandomValue(int x, int y)
        {

            Random random = new Random();
            var coordinateCounter = 0;
            var loopCounter = 0;
            do
            {
                var randomNumber = random.Next(1, GameBoardRoot + 1);
                coordinateCounter++;
                loopCounter++;
                loopCounter = ClearBoard(loopCounter);
                if (coordinateCounter == 9)
                {
                    y = x == 9 && y < 9 ? y + 1 : y;
                    x = x < 9 ? x + 1 : x;

                    coordinateCounter = 0;
                }



                if (!_gameBoard.Any(t => (t.Coordinates.X == x && t.Value == randomNumber) ||
                                        (t.Coordinates.Y == y && t.Value == randomNumber) ||
                                        t.Coordinates.X == x && t.Coordinates.Y == y ||
                                        t.GameBoardRegion == new GameBoardTag(new Coordinates(x, y)).GameBoardRegion
                                        && t.Value == randomNumber))
                {

                    return new GameBoardTag(new Coordinates(x, y), randomNumber);
                }

            } while (true);

        }

        public int ClearBoard(int loopCounter)
        {
            if (loopCounter == 100) Console.Write(loopCounter);
            if (loopCounter == 100)
            {
                if (_gameBoard.Count>89)
                {
                    Console.WriteLine(_gameBoard.Count);
                }
               
                _gameBoard.Clear();
                return 0;
            }

            return loopCounter;


        }


        public void GenerateGame()
        {
            int x = 1;
            int y = 1;
            var random = new Random(9);
            do
            {
                var randomX = random.Next(1, 10);
                var randomY = random.Next(1, 10);
                var tag = GetRandomValue(randomX, randomY);
                _gameBoard.Add(tag);

                
            } while (_gameBoard.Count < 60);
        }

        public void GenerateBlankGame()
        {
            int x = 1;
            int y = 1;

            for (int i = 0; i < _gameBoard.GameBoardSize; i++)
            {
                _gameBoard.Add(new GameBoardTag(new Coordinates(x, y)));
                x++;
                if (x == _gameBoard.GameBoardRoot + 1)
                {
                    x = 1;
                    y++;
                    if (y == _gameBoard.GameBoardRoot + 1)
                    {
                        y = 1;
                    }
                }

            }
        }


    }
}
