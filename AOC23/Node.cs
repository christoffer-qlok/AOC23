using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC23
{
    internal class Node
    {
        public Coord Pos { get; set; }
        public Coord Prev { get; set; }
        public int Depth { get; set; }

        public Node(Coord pos, Coord prev, int depth)
        {
            Pos = pos;
            Prev = prev;
            Depth = depth;
        }

        public Node(int y, int x, int depth)
        {
            Pos = new Coord(y, x);
            Prev = new Coord(y - 1, x);
            Depth = depth;
        }

        public List<Node> GetNeighbours(char[][] map)
        {
            return Pos.GetNeighbours(Prev, map).Select(n => new Node(n, Pos, Depth + 1)).ToList();
        }
    }
}
