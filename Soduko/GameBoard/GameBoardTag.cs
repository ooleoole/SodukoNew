using System;
using System.Collections.Generic;

namespace Soduko.GameBoard
{
    public class GameBoardTag
    {
        private readonly Coordinates _coordinates;
        private readonly int? _value;
        private Region _gameBoardRegion;

        public Coordinates Coordinates => _coordinates;
        public int? Value => _value;
        public Region GameBoardRegion => _gameBoardRegion;
        public GameBoardTag(Coordinates coordinates, int? value)
        {
            _coordinates = coordinates;
            SetGameBoardRegion();
            _value = value;

        }

        public GameBoardTag(Coordinates coordinates)
        {
            _coordinates = coordinates;
            SetGameBoardRegion();
            _value = null;
        }

        public override string ToString()
        {
            return Value == null ? "-" : Value.ToString();
        }

        private void SetGameBoardRegion()
        {



            if (Coordinates.X >= 1 && Coordinates.X <= 3)
            {
                if (Coordinates.Y >= 1 && Coordinates.Y <= 3)
                    _gameBoardRegion = Region.One;
                else if (Coordinates.Y >= 4 && Coordinates.Y <= 6)
                    _gameBoardRegion = Region.Four;
                else if (Coordinates.Y >= 7 && Coordinates.Y <= 9)
                    _gameBoardRegion = Region.Seven;
               
                
            }
            else if (Coordinates.X >= 4 && Coordinates.X <= 6)
            {
                if (Coordinates.Y >= 1 && Coordinates.Y <= 3)
                    _gameBoardRegion = Region.Two;
                else if (Coordinates.Y >= 4 && Coordinates.Y <= 6)
                    _gameBoardRegion = Region.Five;
                else if (Coordinates.Y >= 7 && Coordinates.Y <= 9)
                    _gameBoardRegion = Region.Eight;
               
            }
            else if (Coordinates.X >= 7 && Coordinates.X <= 9)
            {
                if (Coordinates.Y >= 1 && Coordinates.Y <= 3)
                    _gameBoardRegion = Region.Three;
                else if (Coordinates.Y >= 4 && Coordinates.Y <= 6)
                    _gameBoardRegion = Region.Six;
                else if (Coordinates.Y >= 7 && Coordinates.Y <= 9)
                    _gameBoardRegion = Region.Nine;
              
               
            }
        }
        public enum Region
        {
            One = 1, Two = 2, Three = 3, Four = 4, Five = 5, Six = 6, Seven = 7, Eight = 8, Nine = 9

        }


    }
}
