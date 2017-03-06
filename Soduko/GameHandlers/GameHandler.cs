using System;
using System.CodeDom;
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


            var coordinatesSeed = GetCoordinatsSeed();

            var loopCounter = 0;
            do
            {
                Random random = new Random(Guid.NewGuid().GetHashCode());

                loopCounter++;
                loopCounter = ClearBoard(loopCounter);

                var valueSeed = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

                if (_gameBoard.All(t => t.Coordinates != new Coordinates(x, y)))
                {

                    do
                    {
                        var randomIndex = random.Next(0, valueSeed.Length);
                        var value = valueSeed[randomIndex];
                        valueSeed = valueSeed.Except(new[] { value }).ToArray();

                        if (!_gameBoard.Any(t => (t.Coordinates.X == x && t.Value == value) ||
                                            (t.Coordinates.Y == y && t.Value == value) ||
                                            t.GameBoardRegion == new GameBoardTag(new Coordinates(x, y)).GameBoardRegion
                                            && t.Value == value))
                        {

                            return new GameBoardTag(new Coordinates(x, y), value);
                        }
                    } while (valueSeed.Length != 0);

                    var coordinate = new Coordinates(x, y);

                    coordinatesSeed = coordinatesSeed.Except(new[] { coordinate }).ToArray();

                   
                    if (coordinatesSeed.Length == 0)
                    {
                        coordinatesSeed = GetCoordinatsSeed();
                        for (int i = 0; i < 10; i++)
                        {
                            var coordX = random.Next(1, _gameBoard.GameBoardRoot + 1);
                            var coordY = random.Next(1, _gameBoard.GameBoardRoot + 1);

                            _gameBoard.RemoveAt(new Coordinates(coordX, coordY));


                        }
                        Console.WriteLine(_gameBoard.Count);
                    }
                    var index = random.Next(0, coordinatesSeed.Length);
                    coordinate = coordinatesSeed[index];
                    x = coordinate.X;
                    y = coordinate.Y;
                }

                else
                {

                    var coordinate = new Coordinates(x, y);

                    coordinatesSeed = coordinatesSeed.Except(new[] { coordinate }).ToArray();
                    if (coordinatesSeed.Length == 0)
                    {
                        coordinatesSeed = GetCoordinatsSeed();
                        for (int i = 0; i < 10; i++)
                        {
                            var coordX = random.Next(1, _gameBoard.GameBoardRoot + 1);
                            var coordY = random.Next(1, _gameBoard.GameBoardRoot + 1);

                            _gameBoard.RemoveAt(new Coordinates(coordX, coordY));


                        }
                        Console.WriteLine(_gameBoard.Count);
                    }
                    var index = random.Next(0, coordinatesSeed.Length);
                    coordinate = coordinatesSeed[index];
                    x = coordinate.X;
                    y = coordinate.Y;
                }


            } while (true);

        }

        public int ClearBoard(int loopCounter)
        {
            if (loopCounter == 1000) Console.Write(".");
            if (loopCounter == 10000000)
            {
                if (_gameBoard.Count > 70)
                {
                    Console.WriteLine(_gameBoard.Count);
                }

                //_gameBoard.Clear();
                return 0;
            }

            return loopCounter;


        }


        public void GenerateGame()
        {
            int x = 1;
            int y = 1;
            var random = new Random(Guid.NewGuid().GetHashCode());
            do
            {
                var randomX = random.Next(1, 10);
                var randomY = random.Next(1, 10);
                var tag = GetRandomValue(randomX, randomY);
                _gameBoard.Add(tag);


            } while (_gameBoard.Count < 81);
        }

        public Coordinates[] GetCoordinatsSeed()
        {
            int x = 1;
            int y = 1;
            var coordinats = new Coordinates[81];
            for (int i = 0; i < _gameBoard.GameBoardSize; i++)
            {
                coordinats[i] = new Coordinates(x, y);
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
            return coordinats;
        }


    }
}
