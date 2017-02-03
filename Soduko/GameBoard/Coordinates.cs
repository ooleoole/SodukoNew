using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soduko.GameBoard
{
    public struct Coordinates
    {
        public readonly int X, Y;
        public Coordinates(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return "(X=" + this.X + ", Y=" + this.Y + ")";
        }

        public override int GetHashCode()
        {
            return (this.X + 2) ^ (this.Y + 2);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Coordinates))
                return false;
            var other = (Coordinates)obj;
            return this == other;
        }

        public static bool operator ==(Coordinates c1, Coordinates c2)
        {
            return c1.X == c2.X && c1.Y == c2.Y;
        }

        public static bool operator !=(Coordinates c1, Coordinates c2)
        {
            return !(c1 == c2);
        }
    }
}
