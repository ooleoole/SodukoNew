using System;
using System.Collections.Generic;
using System.Linq;
using Soduko.GameBoard;
using Soduko.Utilitys;

namespace Soduko.GameHandlers
{
    public class GameTagDistributor
    {

        private readonly Random _random;
        private readonly IGameBoard _gameBoard;

        private readonly IGameBoard _startingBase;
        public GameTagDistributor(IGameBoardHolder boardHolder)
        {
            _startingBase = boardHolder.GameBoard.Clone();
            _gameBoard = boardHolder.GameBoard;
            _random = new Random(Guid.NewGuid().GetHashCode());
        }

        public void PlaceGameTags()
        {
            var startTime = DateTime.UtcNow;
            RemoveStartingBaseFromCoordinatesSeed();
            do
            {
                RemoveStartingBaseFromCoordinatesSeed();
                var coordinate = GetRandomCoordinatesFromSeed();
                RemoveCoordinatesFromSeed(coordinate);

                var valueSeed = GetValueSeed();
                if (_gameBoard.Where(t => t.Value != null).Any(t => t.Coordinate != coordinate) || _gameBoard.Count == 0)
                {
                    do
                    {
                        var value = GetRandomValueFromSeed(valueSeed);
                        valueSeed = RemoveValueFromSeed(valueSeed, value);
                        var tag = new GameBoardTag(coordinate, value);
                        if (ValidateGameBoardTag(tag))
                        {
                            _gameBoard.AddOrReplace(tag);
                            break;
                        }

                    } while (valueSeed.Length != 0);
                }
                BackTrackIfCoordinatesSeedIsEmpty();
            } while (_gameBoard.Count(t => t.Value != null) < 81 && DateTime.UtcNow - startTime < TimeSpan.FromSeconds(25));

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

        private bool ValidateGameBoardTag(GameBoardTag tag)
        {
            if (_startingBase.Any(t => t.Coordinate == tag.Coordinate && t.Value != null))
            {
                return false;
            }
            return !_gameBoard.Any(t => (t.Coordinate.X == tag.Coordinate.X && t.Value == tag.Value) ||
                                        (t.Coordinate.Y == tag.Coordinate.Y && t.Value == tag.Value) ||
                                        t.GameBoardRegion == tag.GameBoardRegion
                                        && t.Value == tag.Value);


        }

        private void BackTrackIfCoordinatesSeedIsEmpty()
        {
            
            if (!_gameBoard.CoordinatesSeed.Any())
            {
                BackTrack();
                Console.Write("./\\");
            }
        }

        private void RemoveStartingBaseFromCoordinatesSeed()
        {
            var startingBaseWhitoutNulls = GetStartingBaseWhitoutNulls();
            for (var index = 0; index < startingBaseWhitoutNulls.Count; index++)
            {
                var tag = startingBaseWhitoutNulls[index];
                RemoveCoordinatesFromSeed(tag.Coordinate);
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

            var index = _random.Next(0, _gameBoard.CoordinatesSeed.Count - 1);
            var coordinate = _gameBoard.CoordinatesSeed.ElementAt(index);
            return coordinate;
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
            _gameBoard.LoadCoordinatesSeed();
            var coordinates = GetBaseCoordinates();
            var index = _random.Next(0, coordinates.Count - 1);
            var coord = coordinates.ElementAt(index);



            return coord;
        }

        private List<Coordinate> GetBaseCoordinates()
        {
            var startingBaseWhitoutNulls = GetStartingBaseWhitoutNulls();
            var baseCoordinates = new List<Coordinate>();
            baseCoordinates.AddRange(startingBaseWhitoutNulls.Select(tag => tag.Coordinate));
            var coordinates = _gameBoard.CoordinatesSeed.Except(baseCoordinates).ToList();
            return coordinates;
        }

        private List<GameBoardTag> GetStartingBaseWhitoutNulls()
        {
            return _startingBase.Where(t => t.Value != null).ToList();
        }
    }
}