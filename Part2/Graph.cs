﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part2
{
    using System;
    using System.Collections.Generic;

    public class Graph
    {
        private char[,] map;
        private Dictionary<Coord, List<Coord>> adjacencyList;
        private int rows, cols;

        public Graph(char[,] map)
        {
            this.map = map;
            rows = map.GetLength(0);
            cols = map.GetLength(1);
            adjacencyList = new Dictionary<Coord, List<Coord>>();
            BuildGraph();
        }

        private void BuildGraph()
        {
            var frontier = new Stack<Coord>();
            var visited = new HashSet<Coord>();
            var prevDict = new Dictionary<Coord, Coord>();
            frontier.Push(new Coord(0, 1));
            while(frontier.Count > 0)
            {
                var cur = frontier.Pop();
                if (visited.Contains(cur))
                    continue;
                visited.Add(cur);
                Coord? prev = prevDict.ContainsKey(cur) ? prevDict[cur] : null;
                var neighbors = GetNeighbors(cur.Y, cur.X, prev);
                adjacencyList[cur] = neighbors;
                for (int i = neighbors.Count()-1; i >= 0; i++)
                {
                    if (visited.Contains(neighbors[i]))
                        continue;
                    frontier.Push(neighbors[i]);
                    prevDict[neighbors[i]] = cur;
                }
            }
        }

        private List<Coord> GetNeighbors(int row, int col, Coord? previous)
        {
            var neighbors = new List<Coord>();
            char cell = map[row, col];

            // Up, Right, Down, Left
            int[] dRow = { -1, 0, 1, 0 };
            int[] dCol = { 0, 1, 0, -1 };
            char[] directions = { '^', '>', 'v', '<' };

            if (cell == '.' || cell == '#' || Array.IndexOf(directions, cell) != -1)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (cell == '.' && previous.HasValue && previous.Value == new Coord(row + dRow[i], col + dCol[i]))
                        continue;

                    if (cell == '.' || cell == directions[i])
                    {
                        int newRow = row + dRow[i];
                        int newCol = col + dCol[i];
                        if (newRow <= 0)
                            continue;
                        if (!IsInBounds(newRow, newCol))
                            continue;
                        var nCell = map[newRow, newCol];

                        if (nCell == '#')
                            continue;

                        if (nCell == directions[(i + 2) % directions.Length])
                            continue;

                        if(newRow == 0 && newCol == 1)
                            Console.WriteLine($"Adding start node as neighbor for {row}, {col}");
                        neighbors.Add(new Coord(newRow, newCol));
                        
                    }
                }
            }

            return neighbors;
        }

        private bool IsInBounds(int row, int col)
        {
            return row >= 0 && row < rows && col >= 0 && col < cols;
        }


        public Dictionary<Coord, List<Coord>> GetAdjacencyList()
        {
            return adjacencyList;
        }

        public void RemoveCycles()
        {
            bool foundCycle = true;
            while (foundCycle)
            {
                foundCycle = false;
                var visited = new HashSet<Coord>();
                var recursionStack = new HashSet<Coord>();

                foreach (var node in adjacencyList.Keys)
                {
                    if (RemoveCyclesDFS(node, visited, recursionStack, null))
                    {
                        foundCycle = true;
                        break; 
                    }
                }
            }
        }

        private bool RemoveCyclesDFS(Coord node, HashSet<Coord> visited, HashSet<Coord> recursionStack, Coord? parentNode)
        {
            if (!visited.Contains(node))
            {
                visited.Add(node);
                recursionStack.Add(node);

                foreach (var neighbor in adjacencyList[node])
                {
                    if (!visited.Contains(neighbor) && RemoveCyclesDFS(neighbor, visited, recursionStack, node))
                    {
                        Console.WriteLine($"Cycle found at {neighbor}");
                        return true;
                    }
                    else if (recursionStack.Contains(neighbor))
                    {
                        adjacencyList[node].Remove(neighbor);
                        return true;
                    }
                }
            }

            recursionStack.Remove(node);
            return false;
        }
    }

}
