using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace mousse
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private double zoomMax = 5;
        private double zoomMin = 1;
        private double zoomSpeed = 0.001;
        private double zoom = 1;

        public static int first; // запоминаем номер первой выбранной вершины
        private static bool selectNode = false; // выбирали ли мы уже до этого вершину

        private static Queue<int> queuePrint;

        DispatcherTimer gameTimer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();
            MainGrid.Focus();
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);
            gameTimer.Start();
            Description_TBlock.Text = "ЛКМ для создания вершины\nДля создания связи между вершинами последовательно нажмите ПКМ по каждой из них\nДля приближения испоьзуйте колесико мыши\nЕсли ребро ориентированное, то на его конце будет маленькая стрелка, будьте внимательны";
        }

        private void NewNode(Object sender, MouseEventArgs e)
        {
            var windowPosition = Mouse.GetPosition(CanvasGrid);
            var nodes = ViewModel.MainViewModel.Nodes;
            nodes.Add(new ViewModel.CustomEllipse
            {
                Fill = "Red",
                XPos = windowPosition.X - 25, // 25 - радиус окружности
                YPos = windowPosition.Y - 25,
                NodeNumber = nodes.Count() + 1,
            });

        }

        private void NewLine(object sender, MouseButtonEventArgs e)
        {
            // получение вершини на которую нажали
            var border = sender as Border;
            var nodes = ViewModel.MainViewModel.Nodes;

            var tag = Convert.ToInt32(border.Tag) - 1;
            // если уже была выбрана одна из вершин
            if (selectNode)
            {
                //при нажании на ту же вершину
                if (first != tag)
                {

                    selectNode = !selectNode; // если выбирается 1 то станет true, а когда выберем 2, то с первой уйдет выделение 
                    
                    // Возвращаем изначальный цвет. Это можно сделать только путем создания новой вершины
                    nodes[first] = new ViewModel.CustomEllipse()
                    {
                        NodeNumber = first + 1,
                        Fill = "Red",
                        XPos = nodes[first].XPos,
                        YPos = nodes[first].YPos,
                        SmezhNodes = nodes[first].SmezhNodes    
                    };

                    LineSettings lineSettings = new LineSettings();
                    lineSettings.Owner = this;
                    lineSettings.ShowDialog();

                    int weight = lineSettings.GetWidthLine();
                    bool orient = lineSettings.GetOrient();
                    if (!orient)
                    {
                        nodes[tag].SmezhNodes.Add(first, weight);
                    }
                    nodes[first].SmezhNodes.Add(tag, weight);


                    var lines = ViewModel.MainViewModel.Lines;
                    // создаем линию
                    var rad = nodes[first].Radius / 2;
                    lines.Add(new ViewModel.CustomArrow(
                        nodes[first].XPos + rad, 
                        nodes[tag].XPos + rad,
                        nodes[first].YPos + rad,
                        nodes[tag].YPos + rad,
                        weight,
                        orient)
                        );
                }
            }
            // если еще ни одна вершина не выбрана
            else {
                selectNode = !selectNode; // если выбирается 1 то станет true, а когда выберем 2, то с первой уйдет выделение 
                first = tag;
                // перекрашиваем вершину в серый
                PainNode(first, "Gray");
            }
        }

        private void GetMatrix_Click(object sender, RoutedEventArgs e)
        {
            var graph = GetMatrix();

            MatrixWindow matrixWindow = new MatrixWindow(graph);
            matrixWindow.Show();
        }

        public static int[,] GetMatrix()
        {
            var nodes = ViewModel.MainViewModel.Nodes;
            int[,] graph = new int[nodes.Count, nodes.Count];

            Debug.WriteLine(nodes.Count);

            foreach (var node in nodes)
            {
                foreach (var n in node.SmezhNodes)
                {
                    graph[node.NodeNumber - 1, n.Key] = n.Value;
                }
            }
            return graph;
        }

        private void CanvasZoom(object sender, MouseWheelEventArgs e)
        {
            zoom += zoomSpeed * e.Delta; // Ajust zooming speed (e.Delta = Mouse spin value )
            if (zoom < zoomMin) { zoom = zoomMin; } // Limit Min Scale
            if (zoom > zoomMax) { zoom = zoomMax; } // Limit Max Scale
            Point mousePos = e.GetPosition(CanvasGrid);

            if (zoom > 1)
            {
                CanvasGrid.RenderTransform = new ScaleTransform(zoom, zoom, mousePos.X, mousePos.Y); // transform Canvas size from mouse position
            }
            else
            {
                CanvasGrid.RenderTransform = new ScaleTransform(zoom, zoom); // transform Canvas size
            }
        }

        private void DefaultZoom(object sender, RoutedEventArgs e)
        {
            CanvasGrid.RenderTransform = new ScaleTransform(1, 1);
        }

        private void DFS_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.MainViewModel.Nodes.Count!=0)
            switch ((string)DFS_Button.Content){
                case "DFS":
                    queuePrint = Algos.DFSMatrix.StartDFS();
                    PainNode(queuePrint.Dequeue(), "Yellow");
                    DFS_Button.Content = "DFS следущий шаг";
                    break;
                case "DFS следущий шаг":
                    PainNode(queuePrint.Dequeue(), "Yellow");
                    if (queuePrint.Count == 0)
                    {
                        DFS_Button.Content = "Очистить";
                    }
                    break;
                case "Очистить":
                    Refresh();
                    DFS_Button.Content = "DFS";
                    break;
            }
        }

        private void Refresh()
        {
            for(int i = 0; i < ViewModel.MainViewModel.Nodes.Count; i++)
            {
                PainNode(i, "Red");
            } 
        }

        private static void PainNode(int node, string collor)
        {
            var wiewNodes = ViewModel.MainViewModel.Nodes;
            wiewNodes[node] = new ViewModel.CustomEllipse
            {
                NodeNumber = node + 1,
                Fill = collor,
                XPos = wiewNodes[node].XPos,
                YPos = wiewNodes[node].YPos,
                SmezhNodes = wiewNodes[node].SmezhNodes
            };
        }
    }
}
