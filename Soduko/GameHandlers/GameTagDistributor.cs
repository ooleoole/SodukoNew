using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Soduko.GameBoard;
using Soduko.Utilitys;

namespace Soduko.GameHandlers
{
    public class GameTagDistributor
    {

        private readonly Random _random;
        private readonly IGameBoard _gameBoard;
        private IGameBoardHolder _boardHolder;
        public GameTagDistributor(IGameBoardHolder boardHolder)
        {
            _boardHolder = boardHolder;
            _gameBoard = boardHolder.GameBoard;
            _random = new Random(Guid.NewGuid().GetHashCode());
        }

        public void PlaceGameTags()
        {
            var startTime = DateTime.UtcNow;
            do
            {
                var coordinate = GetRandomCoordinatesFromSeed();
                RemoveCoordinatesFromSeed(coordinate);

                var valueSeed = GetValueSeed();
                if (_gameBoard.All(t => t.Coordinate != coordinate))
                {
                    do
                    {
                        var value = GetRandomValueFromSeed(valueSeed);
                        valueSeed = RemoveValueFromSeed(valueSeed, value);

                        if (ValidateGameBoardTag(value, coordinate))
                        {
                            _gameBoard.Add(new GameBoardTag(coordinate, value));
                            break;
                        }


                    } while (valueSeed.Length != 0);
                }
                BackTrackIfCoordinatesSeedIsEmpty();
            } while (_gameBoard.Count < _gameBoard.GameBoardSize && DateTime.UtcNow - startTime < TimeSpan.FromSeconds(5));
        }

        private int[] GetValueSeed()
        {
            var valueSeed = new int[_gameBoard.GameBoardRoot];
            for (int i = 0; i < _gameBoard.GameBoardRoot; i++)
            {
                valueSeed[i] = i + 1;
            }
            return valueSeed;
        }

        public void RemoveRandomGameTagValue()
        {
            int randomIndex = _random.Next(0, _gameBoard.CoordinatesSeed.Count);
            var coordinate = _gameBoard.CoordinatesSeed.ElementAt(randomIndex);
            RemoveCoordinatesFromSeed(coordinate);
            _gameBoard.Replace(new GameBoardTag(coordinate));


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
            if (!_gameBoard.CoordinatesSeed.Any())
            {
                BackTrack();
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
            _gameBoard.CoordinatesSeed.Remove(coordinate);

        }

        private Coordinate GetRandomCoordinatesFromSeed()
        {
            var index = _random.Next(0, _gameBoard.CoordinatesSeed.Count);
            var coordinates = _gameBoard.CoordinatesSeed.ElementAt(index);
            return coordinates;
        }

        private void BackTrack()
        {
            _gameBoard.LoadFreeCoordinatesSeed();
            RemoveRandomCoordinateFromGameBoard();

        }

        private void RemoveRandomCoordinateFromGameBoard()
        {
            var coordinates = GetRandomCoordinate();
            _gameBoard.RemoveAt(coordinates);
        }

        private Coordinate GetRandomCoordinate()
        {
            var coordX = _random.Next(1, _gameBoard.GameBoardRoot + 1);
            var coordY = _random.Next(1, _gameBoard.GameBoardRoot + 1);
            return new Coordinate(coordX, coordY);
        }



    }
}