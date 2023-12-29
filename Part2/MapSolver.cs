using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part2
{
    using System.Collections.Generic;

    public class MapSolver
    {
        private char[,] map;
        private int rows, cols;
        private bool[,] visited;
        private int maxLength = 0;
        private Coord startPoint;
        private Coord endPoint;

        public MapSolver(char[,] map, Coord startPoint, Coord endPoint)
        {
            this.map = map;
            this.startPoint = startPoint;
            this.endPoint = endPoint;
            rows = map.GetLength(0);
            cols = map.GetLength(1);
            visited = new bool[rows, cols];
        }

        public int FindLongestPath()
        {
            var stack = new Stack<Node>();
            stack.Push(new Node(startPoint, new Coord(startPoint.Y - 1, startPoint.X), new HashSet<Coord>(), 0));

            while (stack.Count > 0)
            {
                var cur = stack.Pop();

                if (cur.Pos == endPoint && cur.Len > maxLength)
                {
                    maxLength = cur.Len;
                    Console.WriteLine($"New max length {maxLength}");
                    continue;
                }

                cur.Visited.Add(cur.Pos);

                // Directions: Up, Right, Down, Left
                int[] dRow = { -1, 0, 1, 0 };
                int[] dCol = { 0, 1, 0, -1 };
                bool first = true;
                for (int i = 0; i < 4; i++)
                {
                    var newPos = new Coord(cur.Pos.Y + dRow[i], cur.Pos.X + dCol[i]);
                    if (cur.Visited.Contains(newPos))
                        continue;
                    if (IsInBounds(newPos) && IsWalkable(newPos))
                    {
                        HashSet<Coord> visited;
                        if(first)
                        {
                            first = false;
                            visited = cur.Visited;
                        } else
                        {
                            visited = new HashSet<Coord>(cur.Visited);
                        }
                        stack.Push(new Node(newPos, cur.Pos, visited, cur.Len + 1));
                    }
                }
            }

            return maxLength;
        }

        private bool IsInBounds(Coord pos)
        {
            return pos.Y >= 0 && pos.Y < rows && pos.X >= 0 && pos.X < cols;
        }

        private bool IsWalkable(Coord pos)
        {
            return map[pos.Y, pos.X] != '#';
        }

        private class Node
        {
            public Coord Pos { get; set; }
            public Coord Prev { get; set; }
            public HashSet<Coord> Visited { get; set; }
            public int Len { get; set; }

            public Node(Coord pos, Coord prev, HashSet<Coord> visited, int len)
            {
                Pos = pos;
                Prev = prev;
                Visited = visited;
                Len = len;
            }
        }
    }

}
