using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Soduko.GameBoard;
using Soduko.Utilitys;

namespace Soduko.GameHandlers
{
    public class GameHandler : IGameHandler
    {
        private readonly IGameBoard _gameBoard;
        private IDictionary<IGameBoard, IGameBoard> _gameBoardGameKeysPair;
        private readonly int _difficultyLevel;
        private ICollection<Coordinate> _coordinatesSeed;
        private readonly Random _random;

        public IDictionary<IGameBoard, IGameBoard> GameBoardGameKeysPair => _gameBoardGameKeysPair;

        public GameHandler(IGameBoard gameBoard, int difficultylevel)
        {
            _gameBoard = gameBoard;
            _difficultyLevel = difficultylevel;
            _random = new Random(Guid.NewGuid().GetHashCode());
            _gameBoardGameKeysPair = new Dictionary<IGameBoard, IGameBoard>();
        }

        private GameBoardTag GetGameTag()
        {
            var randomX = _random.Next(1, 10);
            var randomY = _random.Next(1, 10);
            var coordinates = new Coordinate(randomX, randomY);
            _coordinatesSeed = GetCoordinatsSeed();

            do
            {
                var valueSeed = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                if (_gameBoard.All(t => t.Coordinate != coordinates))
                {
                    do
                    {
                        var value = GetRandomValueFromSeed(valueSeed);
                        valueSeed = RemoveValueFromSeed(valueSeed, value);

                        if (ValidateGameBoardTag(value, coordinates))
                            return new GameBoardTag(coordinates, value);

                    } while (valueSeed.Length != 0);

                    RemoveCoordinatesFromSeed(coordinates);
                    BackTrackIfCoordinatesSeedIsEmpty();
                    coordinates = GetRandomCoordinatesFromSeed();
                }

                else
                {
                    RemoveCoordinatesFromSeed(coordinates);
                    BackTrackIfCoordinatesSeedIsEmpty();
                    coordinates = GetRandomCoordinatesFromSeed();
                }

            } while (true);
        }

        private void ClearRandomValuesBasedOnDifficulty()
        {
            var removeAmount = 25 - _difficultyLevel;
            var removeSeed = GetCoordinatsSeed();
            int index;

            for (var i = 0; i < removeAmount; i++)
            {
                index = _random.Next(0, removeSeed.Count);
                removeSeed.RemoveAt(index);
            }

            do
            {
                index = _random.Next(0, removeSeed.Count);
                var randomCoordinate = removeSeed.ElementAt(index);
                removeSeed.Remove(randomCoordinate);
                var gameTag = new GameBoardTag(randomCoordinate);
                _gameBoard.Replace(gameTag);
            } while (removeSeed.Count != 0);

        }
        private bool ValidateGameBoardTag(int value, Coordinate coordinate)
        {
            return !_gameBoard.Any(t => (t.Coordinate.X == coordinate.X && t.Value == value) ||
                                        (t.Coordinate.Y == coordinate.Y && t.Value == value) ||
                                        t.GameBoardRegion == new GameBoardTag(coordinate).GameBoardRegion
                                        && t.Value == value);
        }
        private void BackTrackIfCoordinatesSeedIsEmpty()
        {
            if (!_coordinatesSeed.Any())
            {
                _coordinatesSeed = BackTrackCoordinatesSeed();
                Console.Write(".");
            }
        }

        private static int[] RemoveValueFromSeed(int[] valueSeed, int value)
        {
            valueSeed = valueSeed.Except(new[] { value }).ToArray();
            return valueSeed;
        }

        private int GetRandomValueFromSeed(int[] valueSeed)
        {
            var randomIndex = _random.Next(0, valueSeed.Length);
            var value = valueSeed[randomIndex];
            return value;
        }

        private void RemoveCoordinatesFromSeed(Coordinate coordinate)
        {
            _coordinatesSeed.Remove(coordinate);

        }

        private Coordinate GetRandomCoordinatesFromSeed()
        {
            var index = _random.Next(0, _coordinatesSeed.Count);
            var coordinates = _coordinatesSeed.ElementAt(index);
            return coordinates;
        }

        private IList<Coordinate> BackTrackCoordinatesSeed()
        {
            var coordinatesSeed = GetCoordinatsSeed();
            var coordinates = GetRandomCoordinate();
            _gameBoard.RemoveAt(coordinates);
            return coordinatesSeed;
        }

        private Coordinate GetRandomCoordinate()
        {
            var coordX = _random.Next(1, _gameBoard.GameBoardRoot + 1);
            var coordY = _random.Next(1, _gameBoard.GameBoardRoot + 1);
            return new Coordinate(coordX, coordY);
        }

        public void GenerateGame()
        {
            do
            {
                var tag = GetGameTag();
                _gameBoard.Add(tag);

            } while (_gameBoard.Count < _gameBoard.GameBoardSize);

            AddKeyGamePairToDic();
            ClearRandomValuesBasedOnDifficulty();
        }

        private void AddKeyGamePairToDic()
        {
            var gameBoardKey = _gameBoard.Clone();
            var gameBoard = _gameBoard;
            var keyGamePair = new KeyValuePair<IGameBoard, IGameBoard>(gameBoardKey, gameBoard);
            _gameBoardGameKeysPair.Add(keyGamePair);
        }

        private IList<Coordinate> GetCoordinatsSeed()
        {
            var x = 0;
            var y = 1;
            var coordinates = new List<Coordinate>(_gameBoard.GameBoardSize);
            for (int i = 0; i < _gameBoard.GameBoardSize; i++)
            {
                x++;
                coordinates.Add(new Coordinate(x, y));

                if (x == _gameBoard.GameBoardRoot)
                {
                    x = 0;

                    if (y == _gameBoard.GameBoardRoot)
                    {
                        y = 0;
                    }
                    y++;
                }
            }
            return coordinates;
        }


    }
}
