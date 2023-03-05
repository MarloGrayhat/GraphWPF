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
using System.Windows.Shapes;

namespace mousse
{
    /// <summary>
    /// Логика взаимодействия для LineSettings.xaml
    /// </summary>
    public partial class LineSettings : Window
    {
        private int WidthLine { 
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
        public LineSettings()
        {
            InitializeComponent();
        }

        private void NewLine_Click(object sender, RoutedEventArgs e)
        {
            WidthLine = Convert.ToInt32(Width_TB.Text);
            Orient = (bool)Yes_RB.IsChecked;
            Debug.WriteLine(Orient);
            this.Close();
        }
    }
}
