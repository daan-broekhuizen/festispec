using Festispec.ViewModel.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Festispec.View.Components
{
    /// <summary>
    /// Interaction logic for GraphDialogBox.xaml
    /// </summary>
    public partial class ChartDialogBox : Window
    {
        public ChartDialogViewModel ViewModel
        {
            get
            {
                ChartDialogViewModel viewModel = (ChartDialogViewModel)Resources["ViewModel"];

                return viewModel;
            }
        }

        public ChartDialogBox()
        {
            InitializeComponent();
        }
    }
}
