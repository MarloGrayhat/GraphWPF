using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using mousse.ViewModel;

namespace mousse.Algos
{
    class Deikstra
    {
        private static int nNodes;
        private static Stack<int> way;  //путь до найденой вершины
        private static Node[] nodes; // тут хранятся каждыая вершина по отдельности 
        private static int[,] graph;
        private static Queue<int> queue = new Queue<int>(); // нужен для BFS
        private static Queue<KeyValuePair<int, string>> queueNodes; // помечать цветом весь наш путь


        public static Queue<KeyValuePair<int, string>> StartDeikstra(int startNode, int findNode)
        {
            way = new Stack<int>();
            queueNodes = new Queue<KeyValuePair<int, string>>();
            graph = MainViewModel.GetMatrix();
            nNodes = graph.GetLength(0);
            nodes = new Node[nNodes];
            for (int i = 0; i < nNodes; i++)
            {
                nodes[i] = new Node();
            }
            nodes[startNode].wieght = 0; // по праву первой 
            Dei(startNode);

            string str = "";
            for (int i = 0; i < nNodes; i++)
            {
                str+=i.ToString()+nodes[i].wieght.ToString()+"\n";
            }
            MessageBox.Show(str);
            if (nodes[findNode].parents != -1) // если из стартовой вершины все же можно дойти до искомой
            {
                way.Push(findNode);
                for (int i = findNode; i != startNode; i=nodes[i].parents)
                {
                    way.Push(i);
                }
                way.Push(startNode);
                str = "";
                for (int i = 0; i < way.Count; i++)
                {
                    str += (way.Pop()+1).ToString() + "->";
                }
            } else
            {
                MessageBox.Show($"из вершины{startNode+1} невозможно попасть в вершину {findNode+1}");
            }
            
            MessageBox.Show(str);

            return queueNodes;
        }

        private static void Dei(int node)
        {
            // массив из весов до вершин и их номера
            var now = MainViewModel.Nodes[node];
            var sn = now.SmezhNodes;
            var sortedDict = sn.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value); // сортировка списка нод по весу
            nodes[node].v = 2;
            queueNodes.Enqueue(new KeyValuePair<int, string>(node, "Blue"));

            foreach(var n in sortedDict)
            {
                //этот иф разлелить, иначе, он он идет на 3 из-за тго, что он уже смотрел его и думает, что так длиннее
                if (nodes[n.Key].v == 0) // мы эту вершину не посещали и не просматривали 
                {
                    queueNodes.Enqueue(new KeyValuePair<int, string>(n.Key, "Gray"));
                    if (nodes[node].wieght + n.Value < nodes[n.Key].wieght)
                    {
                        nodes[n.Key].wieght = nodes[node].wieght + n.Value;
                        nodes[n.Key].parents = node;
                    }
                    nodes[n.Key].v = 1;
                    queue.Enqueue(n.Key);
                }else if (nodes[n.Key].v == 1) // мы эту вершину уже смотрели, но нужно проверить и другой исход
                {
                    if (nodes[node].wieght + n.Value < nodes[n.Key].wieght)
                    {
                        nodes[n.Key].wieght = nodes[node].wieght + n.Value;
                        nodes[n.Key].parents = node;
                    }
                }
            }
            if (queue.Count != 0)
            {
                Dei(queue.Dequeue());
            }
        }
    }
}
