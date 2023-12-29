namespace Other
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");
            var map = new char[lines.Length, lines[0].Length];
            var dists = new int[map.GetLength(0), map.GetLength(1)];
            for (int y = 0; y < lines.Length; y++)
            {
                var chars = lines[y].ToCharArray();
                for (int x = 0; x < chars.Length; x++)
                {
                    map[y,x] = chars[x];
                    dists[y, x] = int.MinValue;
                }
            }

            var g = new Graph(map);
            g.RemoveCycles();
            var ts = new TopologicalSort(g, new Coord(0,1));
            var nodes = ts.Sort().ToList();
            var neighbors = g.GetAdjacencyList();
            var first = nodes[0];
            var last = nodes.Last();

            Console.WriteLine($"First: {first}; Last: {last}");

            dists[first.Y, first.X] = 0;

            foreach (var node in nodes)
            {
                foreach (var neighbor in neighbors[node])
                {
                    dists[neighbor.Y, neighbor.X] = Math.Max(dists[neighbor.Y, neighbor.X], dists[node.Y, node.X] + 1);
                }
            }

            Console.WriteLine(dists[last.Y, last.X]);
        }
    }
}