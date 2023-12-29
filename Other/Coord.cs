using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Other
{
    public struct Coord
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Coord(int y, int x)
        {
            X = x;
            Y = y;
        }

        public Coord()
        {
            X = 0;
            Y = 0;
        }

        public override bool Equals(object? obj)
        {
            return obj is Coord coord &&
                   X == coord.X &&
                   Y == coord.Y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        public static Coord operator +(Coord c1, Coord c2)
        {
            return new Coord(c1.Y + c2.Y, c1.X + c2.X);
        }

        public static Coord operator *(int i, Coord c)
        {
            return new Coord(c.Y * i, c.X * i);
        }

        public static Coord operator *(Coord c, int i)
        {
            return new Coord(c.Y * i, c.X * i);
        }

        public static bool operator ==(Coord c1, Coord c2)
        {
            return c1.Equals(c2);
        }

        public static bool operator !=(Coord c1, Coord c2)
        {
            return !c1.Equals(c2);
        }

        public override string ToString()
        {
            return $"({Y},{X})";
        }

        public List<Coord> GetNeighbours(Coord prev, char[][] map)
        {
            Coord[] dirs = new Coord[] { new Coord(0, -1), new Coord(0, 1), new Coord(-1, 0), new Coord(1, 0) };
            var ret = new List<Coord>();

            switch (map[Y][X])
            {
                case '<':
                    ret.Add(dirs[0] + this);
                    return ret;
                case '>':
                    ret.Add(dirs[1] + this);
                    return ret;
                case '^':
                    ret.Add(dirs[2] + this);
                    return ret;
                case 'v':
                    ret.Add(dirs[3] + this);
                    return ret;
                default:
                    break;
            }

            foreach (var dir in dirs)
            {
                var newCoord = this + dir;
                if (newCoord == prev)
                    continue;
                if (".<>^v".Contains(map[newCoord.Y][newCoord.X]))
                    continue;
                ret.Add(newCoord);
            }
            return ret;
        }
    }
}
