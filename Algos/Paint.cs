using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace mousse.Algos
{
    class Paint
    {
        private static int nNodes;
        private static Node[] nodes; // тут хранятся каждыая вершина по отдельности 
        private static int[,] graph;
        private static Queue<KeyValuePair<int, string>> queueNodes; // нужен для BFS
        private static bool twoDol = true;

        // https://e-maxx.ru/algo/bipartite_checking
        // https://ru.algorithmica.org/cs/graph-traversals/bipartite/
        public static Queue<KeyValuePair<int, string>> StartPaint()
        {
            queueNodes = new Queue<KeyValuePair<int, string>>();
            graph = ViewModel.MainViewModel.GetMatrix();
            nNodes = graph.GetLength(0);
            nodes = new Node[nNodes];
            for (int i = 0; i < nNodes; i++)
            {
                nodes[i] = new Node();
            }
            for(int i = 0; i < nNodes; i++)
            {
                for (int j = 0; j < nNodes; j++)
                {
                    if(graph[i, j] == 1)
                    {
                        graph[j, i] = 1;
                    }
                }
            }
            for (int i = 0; i < nNodes; i++)
            {
                if (!nodes[i].visible)  // будем запускать поиск в ширину из каждой непосещённой вершины
                {
                    PaintGraph(i, 1); 
                }
            }
            MessageBox.Show(twoDol.ToString());
            return queueNodes;
        }


        private static void PaintGraph(int node, int color) // обход графа в глубину
        {
            queueNodes.Enqueue(new KeyValuePair<int, string>(node, color == 1 ? "Aqua" : "Blue"));
            nodes[node].visible = true;
            nodes[node].v = color;
            for (int i = 0; i < nNodes; i++)
            {
                if (graph[node, i] != int.MaxValue) 
                {
                    if (nodes[i].v == 0) // если мы идём в какую-то новую вершину
                    {
                        PaintGraph(i, -color); // то мы помещаем её в долю, отличную от доли текущей вершину
                    } else if (nodes[i].v == color) // Если же мы пытаемся пройти по ребру в вершину, которая уже посещена, то мы проверяем, чтобы эта вершина и текущая вершина находились в разных долях. В противном случае граф двудольным не является.
                    {
                        twoDol = false;
                        return;
                    }
                }
            }
        }
    }
}