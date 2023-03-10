using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для LineSettingsWindow.xaml
    /// </summary>
    public partial class LineSettingsWindow : Window
    {
        private int WidthLine
        {
            get;
            set;
        }
        internal int GetWidthLine()
        {
            return WidthLine;
        }
        private bool Orient
        {
            get;
            set;
        }
        internal bool GetOrient()
        {
            return Orient;
        }
        public LineSettingsWindow()
        {
            InitializeComponent();
        }
        private void NewLine_Click(object sender, RoutedEventArgs e)
        {
            WidthLine = Convert.ToInt32(Width_TB.Text);
            Orient = (bool)Yes_RB.IsChecked;
            this.Close();
        }
    }
}
