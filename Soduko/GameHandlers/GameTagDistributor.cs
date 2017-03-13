using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Soduko.GameBoard;
using Soduko.Interfaces;
using Soduko.Utilitys;

namespace Soduko.GameHandlers
{
    public class GameTagDistributor
    {

        private readonly Random _random;
        private readonly IGameBoard _gameBoard;
        private IGameBoardHolder _boardHolder;
        private IGameBoard _startingBase;
        public GameTagDistributor(IGameBoardHolder boardHolder)
        {

            _boardHolder = boardHolder;
            _startingBase = boardHolder.GameBoard.Clone();
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
                if (_gameBoard.Where(t => t.Value != null).Any(t => t.Coordinate != coordinate) || _gameBoard.Count == 0)
                {
                    do
                    {
                        var value = GetRandomValueFromSeed(valueSeed);
                        valueSeed = RemoveValueFromSeed(valueSeed, value);
                        var tag = new GameBoardTag(coordinate, value);
                        if (ValidateGameBoardTag(tag))
                        {
                            if (_startingBase.Contains(tag))
                            {
                                throw new ArgumentException();
                            }
                            _gameBoard.AddOrReplace(tag);
                            break;
                        }


                    } while (valueSeed.Length != 0);
                }
                BackTrackIfCoordinatesSeedIsEmpty();
            } while (_gameBoard.Count(t => t.Value != null) < 81 && DateTime.UtcNow - startTime < TimeSpan.FromSeconds(1000));
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
            if (_startingBase.Contains(tag))
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
            if (_startingBase.Any(t=>t.Coordinate==coordinates&&t.Value!=null))
            {
                throw new AccessViolationException();
            }
            _gameBoard.RemoveAt(coordinates);
        }

        private Coordinate GetRandomCoordinate()
        {
            Coordinate coord;
         
            var Teste = new List<Coordinate>();




            var test = _startingBase.Where(t => t.Value != null).ToList();

            Teste.AddRange(test.Select(tag => tag.Coordinate));
            _gameBoard.LoadCoordinatesSeed();
            var coordinates = _gameBoard.CoordinatesSeed;
            var testetet = coordinates.Except(Teste);
            var index = _random.Next(1, testetet.Count());
            coord = testetet.ElementAt(index);



            return coord;
        }



    }
}