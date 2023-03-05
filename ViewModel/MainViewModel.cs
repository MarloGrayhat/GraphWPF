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
    }
}
