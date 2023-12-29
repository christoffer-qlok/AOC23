using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part2
{
    internal class ShrinkGraph
    {
        private Dictionary<Coord, List<Path>> connected = new Dictionary<Coord, List<Path>>();
        private char[,] map;
        private HashSet<Coord> visited = new HashSet<Coord>();

        public ShrinkGraph(char[,] map)
        {
            this.map = map;
        }

        public void Shrink()
        {
            var frontier = new Stack<Path>();
            frontier.Push(new Path(new Coord(0, 1), new Coord(1, 0)));
            connected[new Coord(0, 1)] = new List<Path>() { frontier.Peek() };
            while(frontier.Count > 0)
            {
                var cur = frontier.Pop();
                
                var neighbors = new List<Coord>() { cur.StartPos + cur.StartDir };
                int steps = 0;
                Coord curPos = cur.StartPos;
                while (neighbors.Count == 1)
                {
                    steps++;
                    curPos = neighbors[0];
                    visited.Add(curPos);
                    neighbors = GetConnected(curPos);
                }
                cur.Len = steps;
                cur.EndPos = curPos;

                connected[curPos] = new List<Path> { new Path(curPos, cur.StartPos, cur.Len) };

                foreach (var neighbor in neighbors)
                {
                    if (visited.Contains(neighbor))
                        continue;
                    Path path = new Path(neighbor, neighbor - curPos);
                    connected[curPos].Add(path);
                    frontier.Push(path);
                }
            }
        }

        private List<Coord> GetConnected(Coord pos)
        {
            var dirs = new Coord[]
            {
                new Coord(1,0),
                new Coord(-1,0),
                new Coord(0,1),
                new Coord(0,-1),
            };
            var ret = new List<Coord>();
            foreach (var dir in dirs)
            {
                var newPos = pos + dir;
                if (visited.Contains(newPos))
                    continue;
                if(IsInBounds(newPos) && IsWalkable(newPos))
                    ret.Add(newPos);
            }
            return ret;
        }

        private bool IsInBounds(Coord pos)
        {
            return pos.Y >= 0 && pos.Y < map.GetLength(0) && pos.X >= 0 && pos.X < map.GetLength(1);
        }

        private bool IsWalkable(Coord pos)
        {
            return map[pos.Y, pos.X] != '#';
        }

        public void PrintConnected()
        {
            Console.WriteLine($"Len: {connected.Count()}");

            foreach (var p in connected)
            {
                Console.WriteLine($"{p.Key}:");
                foreach (var path in p.Value)
                {
                    Console.WriteLine($"\t{path.EndPos} {path.Len}");
                }
            }
        }

        private class Path
        {
            public int Len { get; set; }
            public Coord StartPos { get; set; }
            public Coord StartDir { get; set; }
            public Coord? EndPos { get; set; }

            public Path(Coord start, Coord dir)
            {
                StartDir = dir;
                StartPos = start;
                Len = 0;
                EndPos = null;
            }

            public Path(Coord start, Coord end, int len)
            {
                StartDir = new Coord(0,0);
                StartPos = start;
                EndPos = end;
                Len = len;
            }
        }
    }
}
