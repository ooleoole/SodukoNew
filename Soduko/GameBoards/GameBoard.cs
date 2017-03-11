using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Soduko.GameBoard;
using Soduko.Utilitys;

namespace Soduko.GameBoards
{
    public class GameBoard : IGameBoard
    {
        private IList<GameBoardTag> _boardTags;
        private readonly int _gameBoardRoot;
        private readonly IList<Coordinate> _coordinatesSeed;

        public IList<Coordinate> CoordinatesSeed => _coordinatesSeed;
        public int Count => _boardTags.Count;
        public int GameBoardRoot => _gameBoardRoot;
        public int GameBoardSize => _gameBoardRoot * _gameBoardRoot;

        public GameBoard(int gameBoardRoot)
        {

            _gameBoardRoot = gameBoardRoot;
            _boardTags = new Collection<GameBoardTag>();
            _coordinatesSeed = new List<Coordinate>(_gameBoardRoot * _gameBoardRoot);
            LoadFreeCoordinatesSeed();
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<GameBoardTag> GetEnumerator()
        {
            return _boardTags.GetEnumerator();
        }
        public void Add(GameBoardTag tag)
        {
            if (tag == null) throw new ArgumentNullException(nameof(tag));
            CheckIfBoardSlotIsFree(tag);
            ValidateTag(tag);
            _boardTags.Add(tag);
            _boardTags = _boardTags.OrderByDescending(r => r.Coordinate.Y).ThenBy(c => c.Coordinate.X).ToList();
        }

        public void Clear()
        {
            _boardTags.Clear();
        }
        public void Replace(GameBoardTag tag)
        {
            ValidateTag(tag);
            RemoveAt(tag.Coordinate);
            _boardTags.Add(tag);
            _boardTags = _boardTags.OrderByDescending(r => r.Coordinate.Y).ThenBy(c => c.Coordinate.X).ToList();
        }
        public bool RemoveAt(Coordinate coordinate)
        {
            ValidateCoordinates(coordinate);
            var tag = _boardTags.FirstOrDefault(t => t.Coordinate == coordinate);
            if (tag == null) return false;

            _boardTags.Remove(tag);
            return true;
        }
        public bool Remove(GameBoardTag tag)
        {
            return _boardTags.Remove(tag);
        }


        private void ValidateCoordinates(Coordinate coordinate)
        {
            if (coordinate.X > _gameBoardRoot || coordinate.Y > _gameBoardRoot ||
                coordinate.X <= 0 || coordinate.Y <= 0)
                throw new ArgumentException("Coordinates out of bounds. " +
                                            $"Max value is: {_gameBoardRoot} " +
                                            "Min value is 1");
        }
        private void ValidateTag(GameBoardTag tag)
        {
            ValidateCoordinates(tag.Coordinate);
            ValidateGameTagValue(tag);

        }

        private void ValidateGameTagValue(GameBoardTag tag)
        {
            if (tag.Value > _gameBoardRoot)
                throw new ArgumentException("Invalid value. " +
                                            "Min value is 1. " +
                                            $"Max value is {_gameBoardRoot}");
        }

        private void CheckIfBoardSlotIsFree(GameBoardTag tag)
        {
            if (_boardTags.Any(t => t.Coordinate.X == tag.Coordinate.X &&
                                    t.Coordinate.Y == tag.Coordinate.Y &&
                                    t.Value != null))
                throw new ArgumentException("Slot is taken");
        }

        public IGameBoard Clone()
        {
            IGameBoard clone = new GameBoard(GameBoardRoot);
            foreach (var tag in _boardTags)
            {
                clone.Add(tag);
            }
            return clone;
        }
        public void LoadCoordinatesSeed()
        {
            var x = 0;
            var y = 1;
            ClearCoordinatesSeed();
            for (int i = 0; i < _gameBoardRoot * _gameBoardRoot; i++)
            {
                x++;
                _coordinatesSeed.Add(new Coordinate(x, y));

                if (x == _gameBoardRoot)
                {
                    x = 0;

                    if (y == _gameBoardRoot)
                    {
                        y = 0;
                    }
                    y++;
                }
            }

        }

        public void LoadFreeCoordinatesSeed()
        {
            LoadCoordinatesSeed();
            ExludePlacedTagsFromSeed();
        }

        private void ExludePlacedTagsFromSeed()
        {
            for (int i = 0; i < _coordinatesSeed.Count; i++)
            {
                var coord = _coordinatesSeed.ElementAt(i);
                for (int j = 0; j < _boardTags.Count; j++)
                {
                    var tag = _boardTags.ElementAt(j);
                    if (tag.Value != null && tag.Coordinate == coord)
                        _coordinatesSeed.Remove(coord);

                }
            }
        }
        private void ClearCoordinatesSeed()
        {
            if (_coordinatesSeed.Count != 0)
            {
                _coordinatesSeed.Clear();
            }
        }
        public override string ToString()
        {
            int counter = 0;
            string output = "";
            foreach (var tag in _boardTags)
            {
                counter++;
                output += tag + " ";

                if (counter == 9)
                {
                    output += "\n";
                    counter = 0;
                }
            }
            return output;
        }
    }
}
