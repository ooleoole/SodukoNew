using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soduko.GameBoard
{
    public class GameBoardTag
    {
        private readonly Coordinates _coordinates;
        private readonly int? _value;
        private GameBoardRegions _gameBoardRegion;

        public Coordinates Coordinates => _coordinates;
        public int? Value => _value;

        public GameBoardTag(Coordinates coordinates, int? value)
        {
            _coordinates = coordinates;
            _value = value;

        }

        public GameBoardTag(Coordinates coordinates)
        {
            _coordinates = coordinates;
            _value = null;
        }

        public override string ToString()
        {
            return Value == null ? "-" : Value.ToString();
        }

        private void SetGameBoardRegion()
        {
            if (Coordinates.X > 0 && Coordinates.Y > 0)
            {

            }
        }
        enum GameBoardRegions
        {
            One = 1, Two = 2, Three = 3, Four = 4, Five = 5, Six = 6, Seven = 7, Eight = 8, Nine = 9

        }


    }
}
