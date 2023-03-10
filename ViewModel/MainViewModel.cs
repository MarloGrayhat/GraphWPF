using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;

namespace mousse.ViewModel
{
    class MainViewModel
    {
        public static ObservableCollection<CustomEllipse> Nodes { get; set; }
        public static ObservableCollection<CustomArrow> Lines { get; set; }
        public MainViewModel()
        {

            Nodes = new ObservableCollection<CustomEllipse>();
            Lines = new ObservableCollection<CustomArrow>();
        }

        public static int[,] GetMatrix()
        {
            var nodes = Nodes;
            int[,] graph = new int[nodes.Count, nodes.Count];
            foreach (var node in nodes)
            {
                for(int i = 0; i < nodes.Count; i++ )
                {
                    if (node.SmezhNodes.ContainsKey(i)) graph[node.NodeNumber - 1, i] = node.SmezhNodes[i];
                    else graph[node.NodeNumber - 1, i] = int.MaxValue;
                }
            }
            return graph;
        }
    }
}
