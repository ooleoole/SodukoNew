using System;
using Soduko.GameBoard;
using Soduko.Interfaces;

namespace Soduko.Utilitys
{
    public class GameBoardTag : IGameBoardTag
    {
        private readonly Coordinate _coordinates;
        private readonly int? _value;
        private Region _gameBoardRegion;

        public Coordinate Coordinate => _coordinates;
        public int? Value => _value;
        public Region GameBoardRegion => _gameBoardRegion;
        public GameBoardTag(Coordinate coordinate, int? value)
        {
            _coordinates = coordinate;
            SetGameBoardRegion();
            _value = value;

        }

        public GameBoardTag(Coordinate coordinate)
        {
            _coordinates = coordinate;
            SetGameBoardRegion();
            _value = null;
        }

        public override string ToString()
        {
            return Value == null ? "-" : Value.ToString();
        }

        private void SetGameBoardRegion()
        {

            if (Coordinate.X >= 1 && Coordinate.X <= 3)
            {
                if (Coordinate.Y >= 1 && Coordinate.Y <= 3)
                    _gameBoardRegion = Region.One;
                else if (Coordinate.Y >= 4 && Coordinate.Y <= 6)
                    _gameBoardRegion = Region.Four;
                else if (Coordinate.Y >= 7 && Coordinate.Y <= 9)
                    _gameBoardRegion = Region.Seven;
            }

            else if (Coordinate.X >= 4 && Coordinate.X <= 6)
            {
                if (Coordinate.Y >= 1 && Coordinate.Y <= 3)
                    _gameBoardRegion = Region.Two;
                else if (Coordinate.Y >= 4 && Coordinate.Y <= 6)
                    _gameBoardRegion = Region.Five;
                else if (Coordinate.Y >= 7 && Coordinate.Y <= 9)
                    _gameBoardRegion = Region.Eight;
            }

            else if (Coordinate.X >= 7 && Coordinate.X <= 9)
            {
                if (Coordinate.Y >= 1 && Coordinate.Y <= 3)
                    _gameBoardRegion = Region.Three;
                else if (Coordinate.Y >= 4 && Coordinate.Y <= 6)
                    _gameBoardRegion = Region.Six;
                else if (Coordinate.Y >= 7 && Coordinate.Y <= 9)
                    _gameBoardRegion = Region.Nine;
            }
        }

        public bool Equals(GameBoardTag other)
        {
            if (other == null) return false;
            return Coordinate == other.Coordinate &&
                   Value == other.Value;

        }
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals(obj as GameBoardTag);
        }

        public override int GetHashCode()
        {
            return (this.Coordinate.X + 2) ^ (this.Coordinate.Y + 2) ^ (Value + 2 ?? 7 + 2);
        }

        public static bool operator ==(GameBoardTag tag1, GameBoardTag tag2)
        {
            if (object.ReferenceEquals(tag1, null))
            {
                return object.ReferenceEquals(tag2, null);
            }

            return tag1.Equals(tag2);
        }

        public static bool operator !=(GameBoardTag tag1, GameBoardTag tag2)
        {
            return !(tag1 == tag2);
        }

        public enum Region
        {
            One = 1, Two = 2, Three = 3, Four = 4, Five = 5, Six = 6, Seven = 7, Eight = 8, Nine = 9

        }


    }
}
