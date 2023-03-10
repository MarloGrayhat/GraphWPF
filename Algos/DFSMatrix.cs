using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace mousse.Algos
{
    class DFSMatrix
    {
        private static int nNodes;
        private static Node[] nodes; // тут хранятся каждыая вершина по отдельности 
        private static int[,] graph;
        private static Queue<int> queue;
        public static Queue<int> StartDFS(int firstNode)
        {
            queue = new Queue<int>();
            graph = ViewModel.MainViewModel.GetMatrix();
            nNodes = graph.GetLength(0);
            nodes = new Node[nNodes];
            for (int i = 0; i < nNodes; i++)
            {
                nodes[i] = new Node();
            }
            DFS(firstNode);
            return queue;
        }
       

        private static void /*bool*/ DFS(int node) // обход графа в глубину
        {
            queue.Enqueue(node);
            // нужно, если добавлять заодно и поиск вершины, но я пока уберу
            //if (node == find)
            //{
            //    return true;
            //}
            nodes[node].visible = true;
            for (int i = 0; i < nNodes; i++)
            {
                if (graph[node, i] != int.MaxValue) // исправить потом
                {
                    if (!nodes[i].visible)
                    {
                        DFS(i); // убрать строку, если будешь раскоментировать остальные
                        // нужно, если добавлять заодно и поиск вершины, но я пока уберу
                        //if (DFS(i, find)==true)
                        //{
                        //    way.Push(i);
                        //    return true;
                        //}
                    }
                }
            }
            // нужно, если добавлять заодно и поиск вершины, но я пока уберу
            //return false;
        }
    }
}
