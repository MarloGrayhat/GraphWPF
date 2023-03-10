using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mousse.Algos
{
    class BFSMatrix
    {
        private static int nNodes;
        private static Stack<int> way;  //путь до найденой вершины
        private static Node[] nodes; // тут хранятся каждыая вершина по отдельности 
        private static int[,] graph;
        private static Queue<int> queue = new Queue<int>(); // нужен для BFS
        private static Queue<KeyValuePair<int, string>> queueNodes; // нужен для BFS


        public static Queue<KeyValuePair<int, string>> StartBFS(int firstNode)
        {
            way = new Stack<int>();
            queueNodes = new Queue<KeyValuePair<int, string>>();
            graph = ViewModel.MainViewModel.GetMatrix();
            nNodes = graph.GetLength(0);
            nodes = new Node[nNodes];
            for (int i = 0; i < nNodes; i++)
            {
                nodes[i] = new Node();
            }
            BFS(firstNode);
            return queueNodes;
        }

        private static /*bool*/void BFS(int node)
        {
            //if (node == find)
            //{
            //    queueNodes.Enqueue(new KeyValuePair<int, string>(node, "Blue"));
            //    return true;
            //}
            nodes[node].v = 2;
            queueNodes.Enqueue(new KeyValuePair<int, string>(node, "Blue"));
            for (int i = 0; i < nNodes; i++)
            {
                if (graph[node, i] != int.MaxValue)
                {
                    if (!(nodes[i].v == 1 || nodes[i].v == 2)) // мы эту вершину и не посещали и не просмтаривали
                    {
                        queueNodes.Enqueue(new KeyValuePair<int, string>(i, "Gray"));
                        nodes[i].v = 1;
                        queue.Enqueue(i);
                    }
                }
            }
            if (queue.Count != 0)
            {
                BFS(queue.Dequeue());
            }
            //if (queue.Count == 0) return false;
            //bool ret = BFS(queue.Dequeue(), find);
            //BFS(queue.Dequeue());
            //if (ret)
            //{
            //    way.Push(node);
            //}
            //return ret;
        }
    }

   

   
}
