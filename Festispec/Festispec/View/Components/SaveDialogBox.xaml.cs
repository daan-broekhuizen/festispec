using Festispec.ViewModel.Components;
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

namespace Festispec.View.Components
{
    /// <summary>
    /// Interaction logic for SaveDialogBox.xaml
    /// </summary>
    public partial class SaveDialogBox : Window
    {
        public SaveDialogViewModel ViewModel
        {
            get
            {
                SaveDialogViewModel viewModel = (SaveDialogViewModel)Resources["ViewModel"];

                return viewModel;
            }
        }
        public SaveDialogBox()
        {
            InitializeComponent();
        }
    }
}
