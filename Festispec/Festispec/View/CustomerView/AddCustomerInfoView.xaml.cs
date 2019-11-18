using Microsoft.Win32;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Festispec.View
{
    /// <summary>
    /// Interaction logic for AddCustomerInfoView.xaml
    /// </summary>
    public partial class AddCustomerInfoView : Page
    {
        public AddCustomerInfoView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Kies een logo";
            op.Filter = "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
                CustomerLogo.Source = new BitmapImage(new Uri(op.FileName));
        }
    }
}
