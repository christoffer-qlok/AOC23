using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part2
{
    public class TopologicalSort
    {
        private Graph graph;
        private Stack<Coord> sortedStack;
        private HashSet<Coord> visited;
        private Coord startNode;

        public TopologicalSort(Graph graph, Coord startNode)
        {
            this.graph = graph;
            this.startNode = startNode;
            sortedStack = new Stack<Coord>();
            visited = new HashSet<Coord>();
        }

        public Stack<Coord> Sort()
        {
            var adjacencyList = graph.GetAdjacencyList();

            if (adjacencyList.ContainsKey(startNode) && !visited.Contains(startNode))
            {
                TopologicalSortUtil(startNode, adjacencyList);
            }

            // Continue with the rest of the nodes
            foreach (var node in adjacencyList.Keys)
            {
                if (!visited.Contains(node))
                {
                    TopologicalSortUtil(node, adjacencyList);
                }
            }
            return sortedStack;
        }

        private void TopologicalSortUtil(Coord node, Dictionary<Coord, List<Coord>> adjacencyList)
        {
            var stack = new Stack<Coord>();
            stack.Push(node);

            while (stack.Count > 0)
            {
                var current = stack.Peek();

                if (!visited.Contains(current))
                {
                    visited.Add(current);
                    foreach (var neighbor in adjacencyList[current])
                    {
                        if (!visited.Contains(neighbor))
                        {
                            stack.Push(neighbor);
                        }
                    }
                }
                else
                {
                    stack.Pop();
                    if (!sortedStack.Contains(current))
                    {
                        sortedStack.Push(current);
                    }
                }
            }
        }
    }


}
