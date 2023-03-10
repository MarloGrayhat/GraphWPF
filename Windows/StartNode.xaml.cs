using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для StartNode.xaml
    /// </summary>
    public partial class StartNode : Window
    {
        public static int firstNode = -1;
        public StartNode()
        {
            InitializeComponent();
        }

        private void StartNode_TB_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void StartNode_BTN_Click(object sender, RoutedEventArgs e)
        {
            var nodes = ViewModel.MainViewModel.Nodes;
            int selectNode = Convert.ToInt32(StartNode_TB.Text) - 1;
            if (nodes.Count <= selectNode)
            {
                MessageBox.Show("Вершины с таким номером нет");
                firstNode = -1; // на всякий случай
            }
            else
            {
                firstNode = selectNode;
                this.Close();
            }
        }
    }
}
