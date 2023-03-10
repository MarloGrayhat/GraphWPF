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
    /// Логика взаимодействия для Description.xaml
    /// </summary>
    public partial class Description : Window
    {
        public Description()
        {
            InitializeComponent();
            Description_TBlock.Text = "ЛКМ для создания вершины\nДля создания связи между вершинами последовательно нажмите ПКМ по каждой из них\nДля приближения испоьзуйте колесико мыши наведя на объект\nЕсли ребро ориентированное, то на его конце будет маленькая стрелка, будьте внимательны";

        }
    }
}
