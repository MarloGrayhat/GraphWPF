using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace mousse.Algos
{
    class Node
    {
        public bool visible = false; // for dfs
        public int v = 0; // 0 - не посещен, 1 - посмотрели и добавили, но не посетили 2 - посетили
    }

    class DFSMatrix
    {
        public static int nowN = 0;
        private static int nNodes;
        private static Stack<int> way = new Stack<int>();  //путь до найденой вершины
        private static Node[] nodes; // тут хранятся каждыая вершина по отдельности 
        private static int[,] graph;
        private static Queue<int> queue = new Queue<int>();
        public static Queue<int> StartDFS()
        {
            graph = MainWindow.GetMatrix();
            nNodes = graph.GetLength(0);
            nodes = new Node[nNodes];
            for (int i = 0; i < nNodes; i++)
            {
                nodes[i] = new Node();
            }
            DFS(nowN, nNodes - 1);
            return queue;
        }
       

        private static bool DFS(int node, int find) // обход графа в глубину
        {
            queue.Enqueue(node);
            if (node == find)
            {
                return true;
            }
            nodes[node].visible = true;
            for (int i = 0; i < nNodes; i++)
            {
                if (graph[node, i] != 0) // исправить потом
                {
                    if (!nodes[i].visible)
                    {
                        if (DFS(i, find)==true)
                        {
                            way.Push(i);
                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }
}
