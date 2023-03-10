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
using mousse.Windows;

namespace mousse
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private double zoomMax = 5;
        private double zoomMin = 0.5;
        private double zoomSpeed = 0.001;
        private double zoom = 1;

        public static int first; // запоминаем номер первой выбранной вершины
        private static bool selectNode = false; // выбирали ли мы уже до этого вершину

        public MainWindow()
        {
            InitializeComponent();
            this.Show();

            #region Вывод информации слева от основного окна
            Description description = new Description();
            description.Owner = this;
            description.WindowStartupLocation = WindowStartupLocation.Manual;
            description.Left = this.Left - description.Description_Window.Width;
            description.Top = this.Top;
            description.Show();
            #endregion

            #region Вывод кнопок справа от основного окна
            SettingsButtons sb = new SettingsButtons(this);
            sb.Owner = this;
            sb.WindowStartupLocation = WindowStartupLocation.Manual;
            sb.Left = this.Left + this.Width;
            sb.Top = this.Top;
            sb.Show();
            #endregion
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
                
                if (first != tag)
                {
                    selectNode = !selectNode; // если выбирается 1 то станет true, а когда выберем 2, то с первой уйдет выделение 

                    // Возвращаем изначальный цвет. Это можно сделать только путем создания новой вершины
                    SettingsButtons.PaintNode(first, "Red");

                    LineSettingsWindow lineSettings = new LineSettingsWindow();
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

                    nodes[tag].SmezhLines.Add(lines.Count - 1);
                    nodes[first].SmezhLines.Add(lines.Count - 1);

                }
                else //при нажании на ту же вершину
                {
                    SettingsButtons.PaintNode(first, "Red");
                    selectNode = !selectNode; // если выбирается 1 то станет true, а когда выберем 2, то с первой уйдет выделение 
                }
            }
            // если еще ни одна вершина не выбрана
            else {
                selectNode = !selectNode; // если выбирается 1 то станет true, а когда выберем 2, то с первой уйдет выделение 
                first = tag;
                // перекрашиваем вершину в серый
                SettingsButtons.PaintNode(first, "Gray");
            }
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

        private void MoveNode(object sender, MouseEventArgs e)
        {
            var border = sender as Border;
            if (border.IsMouseCaptured )
            {
                var nodes = ViewModel.MainViewModel.Nodes;
                var lines = ViewModel.MainViewModel.Lines;
                var tag = Convert.ToInt32(border.Tag) - 1;
                var mp = e.GetPosition(CanvasGrid);
                Debug.WriteLine(mp.X + " " + mp.Y);
                var nodeXPos = nodes[tag].XPos;
                var nodeYPos = nodes[tag].YPos;
                if (mp.X > 0 && mp.Y > 0 && mp.X < CanvasGrid.ActualWidth && mp.Y < CanvasGrid.ActualHeight)
                {
                    foreach (var line in nodes[tag].SmezhLines)
                    {
                        // нужно сохранять ориентированность графа, только поэтому тут эта проверка, я уверен, что это можно сделать короче, но мне лень.
                        bool orient = lines[line].StrokeEndLineCap == "Triangle";
                        if (nodeXPos + 25 == lines[line].X1MainLine && nodeYPos + 25 == lines[line].Y1MainLine)
                        {
                            lines[line] = new ViewModel.CustomArrow(
                            mp.X,
                            lines[line].X2MainLine,
                            mp.Y,
                            lines[line].Y2MainLine,
                            lines[line].WeightText,
                            orient
                            );
                        }
                        else
                        {
                            lines[line] = new ViewModel.CustomArrow(
                            lines[line].X1MainLine,
                            mp.X,
                            lines[line].Y1MainLine,
                            mp.Y,
                            lines[line].WeightText,
                            !orient
                            );
                        }

                    }
                    nodes[tag] = new ViewModel.CustomEllipse
                    {
                        NodeNumber = tag + 1,
                        Fill = "Pink",
                        XPos = mp.X - 25,
                        YPos = mp.Y - 25,
                        SmezhNodes = nodes[tag].SmezhNodes,
                        SmezhLines = nodes[tag].SmezhLines
                    };
                }
            }
        }

        protected void DrugNode(object sender, MouseButtonEventArgs e)
        {
            Mouse.Capture(sender as Border);
        }

        protected void DropNode(object sender, MouseButtonEventArgs e)
        {
            Mouse.Capture(null);
            var border = sender as Border;
            var tag = Convert.ToInt32(border.Tag) - 1;
            SettingsButtons.PaintNode(tag, "Red");
        }
    }
}
