using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Soduko.GameBoard
{
    public class GameBoard2 : IEnumerable<GameBoardTag>
    {
        private readonly Collection<GameBoardTag> _boardTags;
        private readonly int _gameBoardRoot;

        public int Count => _boardTags.Count;
        public int GameBoardRoot => _gameBoardRoot;
        public int GameBoardSize => _gameBoardRoot * _gameBoardRoot;
        public GameBoard2(int gameBoardRoot)
        {
            _gameBoardRoot = gameBoardRoot;
            _boardTags = new Collection<GameBoardTag>();
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
        }

        public void Clear()
        {
            _boardTags.Clear();
        }
        public void Replace(GameBoardTag tag)
        {
            ValidateTag(tag);
            RemoveAt(tag.Coordinates);
            _boardTags.Add(tag);
        }
        public bool RemoveAt(Coordinates coordinates)
        {
            ValidateCoordinates(coordinates);
            var tag = _boardTags.FirstOrDefault(t => t.Coordinates == coordinates && t.Value != null);
            if (tag == null) return false;

            _boardTags.Remove(tag);
            return true;
        }
        public bool Remove(GameBoardTag tag)
        {
            return _boardTags.Remove(tag);
        }


        private void ValidateCoordinates(Coordinates coordinates)
        {
            if (coordinates.X > _gameBoardRoot || coordinates.Y > _gameBoardRoot ||
                coordinates.X <= 0 || coordinates.Y <= 0)
                throw new ArgumentException("Coordinates out of bounds. " +
                                            $"Max value is: {_gameBoardRoot} " +
                                            "Min value is 1");
        }
        private void ValidateTag(GameBoardTag tag)
        {
            ValidateCoordinates(tag.Coordinates);
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
            if (_boardTags.Any(t => t.Coordinates.X == tag.Coordinates.X &&
                                    t.Coordinates.Y == tag.Coordinates.Y &&
                                    t.Value != null))
                throw new ArgumentException("Slot is taken");
        }

        
    }
}
