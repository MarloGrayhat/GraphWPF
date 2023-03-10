using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace mousse.Windows
{
    /// <summary>
    /// Логика взаимодействия для SettingsButtons.xaml
    /// </summary>
    public partial class SettingsButtons : Window
    {
        private static Queue<int> queueNodes;
        private static Queue<KeyValuePair<int, string>> queueNodesWithColor;
        private static MainWindow mainWindow;
        public SettingsButtons(MainWindow mw)
        {
            mainWindow = mw;
            InitializeComponent();
        }

        private void GetMatrix_Click(object sender, RoutedEventArgs e)
        {
            var graph = ViewModel.MainViewModel.GetMatrix();


            MatrixWindow matrixWindow = new MatrixWindow(graph);
            matrixWindow.Owner = this;
            matrixWindow.WindowStartupLocation = WindowStartupLocation.Manual;
            matrixWindow.Left = this.Left;
            matrixWindow.Top = this.Top+this.Width+60;
            matrixWindow.Show();

            matrixWindow.Show();
        }
        

        private void DefaultZoom(object sender, RoutedEventArgs e)
        {
            mainWindow.CanvasGrid.RenderTransform = new ScaleTransform(1, 1);
        }

        private void Refresh()
        {
            for (int i = 0; i < ViewModel.MainViewModel.Nodes.Count; i++)
            {
                PaintNode(i, "Red");
            }
        }
        private void DFS_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.MainViewModel.Nodes.Count != 0)
                switch ((string)DFS_Button.Content)
                {
                    case "DFS":
                        var sn = new StartNode();
                        sn.Owner = mainWindow;
                        sn.ShowDialog();
                        queueNodes = Algos.DFSMatrix.StartDFS(StartNode.firstNode);
                        PaintNode(queueNodes.Dequeue(), "Yellow");
                        DFS_Button.Content = "DFS следущий шаг";
                        BFS_Button.IsEnabled = false;
                        Paint_Button.IsEnabled = false;
                        break;
                    case "DFS следущий шаг":
                        PaintNode(queueNodes.Dequeue(), "Yellow");
                        if (queueNodes.Count == 0)
                        {
                            DFS_Button.Content = "Очистить";
                        }
                        break;
                    case "Очистить":
                        Refresh();
                        queueNodes = new Queue<int>();
                        DFS_Button.Content = "DFS";
                        BFS_Button.IsEnabled = true;
                        Paint_Button.IsEnabled = true;
                        break;
                }
        }
        private void BFS_Click(object sender, RoutedEventArgs e)
        {
            switch ((string)BFS_Button.Content)
            {
                case "BFS":
                    var sn = new StartNode();
                    sn.Owner = mainWindow;
                    sn.ShowDialog();
                    queueNodesWithColor = Algos.BFSMatrix.StartBFS(StartNode.firstNode);
                    var node = queueNodesWithColor.Dequeue();
                    PaintNode(node.Key, node.Value);
                    BFS_Button.Content = "BFS следущий шаг";
                    DFS_Button.IsEnabled = false;
                    Paint_Button.IsEnabled = false;
                    break;
                case "BFS следущий шаг":
                    node = queueNodesWithColor.Dequeue();
                    PaintNode(node.Key, node.Value);
                    if (queueNodesWithColor.Count == 0)
                    {
                        BFS_Button.Content = "Очистить";
                    }
                    break;
                case "Очистить":
                    Refresh();
                    queueNodesWithColor = new Queue<KeyValuePair<int, string>>();
                    BFS_Button.Content = "BFS";
                    DFS_Button.IsEnabled = true;
                    Paint_Button.IsEnabled = true;
                    break;
            }
        }
        private void Paint_Click(object sender, RoutedEventArgs e)
        {
            switch ((string)Paint_Button.Content)
            {
                case "Проверка на двудольность":
                    queueNodesWithColor = Algos.Paint.StartPaint();
                    if (queueNodesWithColor.Count != 0)
                    {
                        var node = queueNodesWithColor.Dequeue();
                        PaintNode(node.Key, node.Value);
                        Paint_Button.Content = "следующий шаг";
                        BFS_Button.IsEnabled = false;
                        DFS_Button.IsEnabled = false;
                    }
                    break;
                case "следующий шаг":
                    if (queueNodesWithColor.Count != 0)
                    {
                        var node = queueNodesWithColor.Dequeue();
                        PaintNode(node.Key, node.Value);
                    } else
                    {
                        Paint_Button.Content = "Очистить";
                    }
                    break;
                case "Очистить":
                    Refresh();
                    queueNodesWithColor = new Queue<KeyValuePair<int, string>>();
                    Paint_Button.Content = "Проверка на двудольность";
                    BFS_Button.IsEnabled = true;
                    DFS_Button.IsEnabled = true;
                    break;
            }
        }
        internal static void PaintNode(int node, string collor)
        {
            var wiewNodes = ViewModel.MainViewModel.Nodes;
            wiewNodes[node] = new ViewModel.CustomEllipse
            {
                NodeNumber = node + 1,
                Fill = collor,
                XPos = wiewNodes[node].XPos,
                YPos = wiewNodes[node].YPos,
                SmezhNodes = wiewNodes[node].SmezhNodes,
                SmezhLines = wiewNodes[node].SmezhLines
            };
        }

        private void ClearAll_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.MainViewModel.Nodes.Clear();
            ViewModel.MainViewModel.Lines.Clear();
        }

        private void Deikstra_Click(object sender, RoutedEventArgs e)
        {
            switch ((string)Deikstra_Button.Content)
            {
                case "Дейкстра":
                    var sn = new StartNode();
                    sn.Owner = mainWindow;
                    sn.ShowDialog();
                    if (StartNode.firstNode != -1)
                    {
                        queueNodesWithColor = Algos.Deikstra.StartDeikstra(StartNode.firstNode, 3);
                        var node = queueNodesWithColor.Dequeue();
                        PaintNode(node.Key, node.Value);
                        Deikstra_Button.Content = "следующий шаг";
                        BFS_Button.IsEnabled = false;
                        DFS_Button.IsEnabled = false;
                    }
                    break;
                case "следующий шаг":
                    if (queueNodesWithColor.Count != 0)
                    {
                        var node = queueNodesWithColor.Dequeue();
                        PaintNode(node.Key, node.Value);
                    }
                    if (queueNodesWithColor.Count == 0)
                    {
                        Deikstra_Button.Content = "Очистить";
                    }
                    break;
                case "Очистить":
                    Refresh();
                    queueNodesWithColor = new Queue<KeyValuePair<int, string>>();
                    Deikstra_Button.Content = "Дейкстра";
                    BFS_Button.IsEnabled = true;
                    DFS_Button.IsEnabled = true;
                    break;
            }
        }
    }
}
