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

        private GameBoardTag GetRandomValue(Coordinates coordinates)
        {
            var coordinatesSeed = GetCoordinatsSeed();

            var loopCounter = 0;
            do
            {
                Random random = new Random(Guid.NewGuid().GetHashCode());

                loopCounter++;
              

                var valueSeed = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

                if (_gameBoard.All(t => t.Coordinates != coordinates))
                {

                    do
                    {
                        var randomIndex = random.Next(0, valueSeed.Length);
                        var value = valueSeed[randomIndex];
                        valueSeed = valueSeed.Except(new[] { value }).ToArray();

                        if (!_gameBoard.Any(t => (t.Coordinates.X == coordinates.X && t.Value == value) ||
                                           (t.Coordinates.Y == coordinates.Y && t.Value == value) ||
                                            t.GameBoardRegion == new GameBoardTag(coordinates).GameBoardRegion
                                            && t.Value == value))
                        {

                            return new GameBoardTag(coordinates, value);
                        }
                    } while (valueSeed.Length != 0);


                    coordinatesSeed = coordinatesSeed.Except(new[] { coordinates }).ToArray();

                   
                    if (coordinatesSeed.Length == 0)
                    {
                        coordinatesSeed = BackTrackCoordinatesSeed(coordinatesSeed, random);
                        Console.Write(".");
                    }
                    var index = random.Next(0, coordinatesSeed.Length);
                    coordinates = coordinatesSeed[index];
                    
                }

                else
                {

                   

                    coordinatesSeed = coordinatesSeed.Except(new[] { coordinates }).ToArray();
                    if (coordinatesSeed.Length == 0)
                    {
                        coordinatesSeed = BackTrackCoordinatesSeed(coordinatesSeed, random);
                        Console.Write(".");
                    }
                    var index = random.Next(0, coordinatesSeed.Length);
                    coordinates = coordinatesSeed[index];
                   
                }


            } while (true);

        }

        private Coordinates[] BackTrackCoordinatesSeed(Coordinates[] coordinatesSeed, Random random)
        {
            coordinatesSeed = GetCoordinatsSeed();
            for (int i = 0; i < 2; i++)
            {
                var coordX = random.Next(1, _gameBoard.GameBoardRoot + 1);
                var coordY = random.Next(1, _gameBoard.GameBoardRoot + 1);

                _gameBoard.RemoveAt(new Coordinates(coordX, coordY));
            }
            return coordinatesSeed;
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
                var tag = GetRandomValue(new Coordinates(randomX,randomY));
                _gameBoard.Add(tag);


            } while (_gameBoard.Count < 81);
        }

        private Coordinates[] GetCoordinatsSeed()
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
