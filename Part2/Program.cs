namespace Part2
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
                    map[y,x] = chars[x] == '#' ? '#' : '.';
                    //Console.Write(map[y,x]);
                    dists[y, x] = int.MinValue;
                }
                //Console.WriteLine();
            }

            var ms = new MapSolver(map, new Coord(0, 1), new Coord(map.GetLength(0) - 1, map.GetLength(1) - 2));
            var l = ms.FindLongestPath();
            Console.WriteLine(l);
        }
    }
}