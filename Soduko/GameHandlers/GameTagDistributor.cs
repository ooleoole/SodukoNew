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
        private readonly IGameBoardRules _boardRules;

        public GameTagDistributor(IGameBoardHolder boardHolder)
        {
            _startingBase = boardHolder.GameBoard.Clone();
            _gameBoard = boardHolder.GameBoard;
            _random = new Random(Guid.NewGuid().GetHashCode());
            _boardRules = boardHolder.BoardRules;
        }

        public void PlaceGameTags()
        {
            var startTime = DateTime.UtcNow;
            var counter = 0;
            do
            {
                //RemoveStartingBaseFromCoordinatesSeed();
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
                        if (_boardRules.ValidateGameBoardTag(tag, _gameBoard, _startingBase))
                        {
                            counter++;
                            _gameBoard.AddOrReplace(tag);
                            break;
                        }

                    } while (valueSeed.Length != 0);
                }
                BackTrackIfCoordinatesSeedIsEmpty();
                if (counter==35000)
                {
                    Console.WriteLine("Not SOlved!!!!!");
                }
            } while (_gameBoard.Count(t => t.Value != null) < 81 && counter < 35000);
            Console.WriteLine(counter);
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
            int randomIndex = _random.Next(_gameBoard.CoordinatesSeed.Count);
            var coordinate = _gameBoard.CoordinatesSeed.ElementAt(randomIndex);
            RemoveCoordinatesFromSeed(coordinate);
            _gameBoard.Replace(new GameBoardTag(coordinate));
        }

        private void BackTrackIfCoordinatesSeedIsEmpty()
        {
            if (!_gameBoard.CoordinatesSeed.Any())
            {
                BackTrack();
                //Console.Write("./\\");
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
            var randomIndex = _random.Next(valueSeed.Length);
            var value = valueSeed[randomIndex];
            return value;
        }

        private void RemoveCoordinatesFromSeed(Coordinate coordinate)
        {
            _gameBoard.CoordinatesSeed.Remove(coordinate);
        }

        private Coordinate GetRandomCoordinatesFromSeed()
        {
            var index = _random.Next(_gameBoard.CoordinatesSeed.Count);
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
            var index = _random.Next(coordinates.Count);
            var coordinate = coordinates.ElementAt(index);
            return coordinate;
        }

        private List<Coordinate> GetBaseCoordinates()
        {
            var startingBaseWhitoutNulls = GetStartingBaseWhitoutNulls();
            var baseCoordinates = new List<Coordinate>();
            baseCoordinates.AddRange(startingBaseWhitoutNulls.Select(tag => tag.Coordinate));
            var coordinates = _gameBoard.CoordinatesSeed.Except(baseCoordinates).ToList();
            //for (int i = 0; i < startingBaseWhitoutNulls.Count; i++)
            //{
            //    var tag = startingBaseWhitoutNulls[i];
            //    for (int j = 0; j < _gameBoard.CoordinatesSeed.Count; j++)
            //    {
            //        var coord = _gameBoard.CoordinatesSeed[j];
            //        if (coord != tag.Coordinate)
            //        {
            //            coordinates.Add(coord);
            //        }
            //    }
            //}
            return coordinates;
        }

        private List<GameBoardTag> GetStartingBaseWhitoutNulls()
        {
            var startingBaseWhitoutNulls = _startingBase.Where(t => t.Value != null).ToList();
            //for (int i = 0; i < _startingBase.Count; i++)
            //{
            //    var tag = _startingBase.ElementAt(i);
            //    if (tag.Value != null)
            //    {
            //        startingBaseWhitoutNulls.Add(tag);
            //    }
            //}
            return startingBaseWhitoutNulls;
        }
    }
}