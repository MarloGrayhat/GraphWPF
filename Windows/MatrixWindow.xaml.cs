using System;
using System.Collections.Generic;
using System.Dynamic;
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
using System.Windows.Shapes;

namespace mousse.Windows
{
    /// <summary>
    /// Логика взаимодействия для MatrixWindow.xaml
    /// </summary>
    public partial class MatrixWindow : Window
    {
        public MatrixWindow(int[,] graph)
        {
            InitializeComponent();
            this.Width = 50+graph.GetLength(0) * 54;
            this.Height = 80 + graph.GetLength(1) * 37;
            DataGridTextColumn column = new DataGridTextColumn();
            column.Header = "№";
            column.Binding = new Binding("nodeNubmer");
            dg.Columns.Add(column);

            for (int i = 0; i < graph.GetLength(1); i++)
            {
                column = new DataGridTextColumn();
                column.Header = i + 1;  // подпись колонки
                column.Binding = new Binding("col" + Convert.ToString(i + 1)); //название колонки для дальнейшего обращения
                dg.Columns.Add(column);
            }

            dynamic row;
            for (int i = 0; i < graph.GetLength(0); i++)
            {
                row = new ExpandoObject();
                ((IDictionary<string, object>)row)["nodeNubmer"] = i + 1;
                for (int j = 0; j < graph.GetLength(1); j++)
                {
                    if (graph[i, j] == int.MaxValue) ((IDictionary<string, object>)row)["col" + Convert.ToString(j + 1)] = "ထ";
                    else ((IDictionary<string, object>)row)["col" + Convert.ToString(j + 1)] = graph[i, j];
                }
                dg.Items.Add(row);
            }
        }
    }
}
