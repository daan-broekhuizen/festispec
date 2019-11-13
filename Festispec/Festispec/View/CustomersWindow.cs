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
using Festispec.ViewModel;

namespace Festispec
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>

    public partial class CustomersWindow : Page
    {
        public CustomersWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        /*        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
                { 
                    CustomerListViewModel customerListViewModel = new CustomerListViewModel();
                    ComboBoxItem cbi = (ComboBoxItem)(sender as ComboBox).SelectedItem;
                    customerListViewModel.SortCustomers(cbi.Content.ToString());
                }*/
    }
}
