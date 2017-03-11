namespace Soduko.Utilitys
{
    public struct Coordinate
    {
        public readonly int X, Y;
        public Coordinate(int x, int y)
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
            if (!(obj is Coordinate))
                return false;
            var other = (Coordinate)obj;
            return this == other;
        }

        public static bool operator ==(Coordinate c1, Coordinate c2)
        {
            return c1.X == c2.X && c1.Y == c2.Y;
        }

        public static bool operator !=(Coordinate c1, Coordinate c2)
        {
            return !(c1 == c2);
        }
    }
}
