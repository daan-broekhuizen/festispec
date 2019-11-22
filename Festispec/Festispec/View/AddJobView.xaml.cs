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
using Festispec.Model.Repositories;
using FestiSpec.Domain.Repositories;

namespace Festispec.View
{
    /// <summary>
    /// Interaction logic for AddJobView.xaml
    /// </summary>
    public partial class AddJobView : Page
    {
        public AddJobView()
        {
            InitializeComponent();
            FillCombo();
        }

        public void FillCombo()
        {
            CustomerRepository Crepo = new CustomerRepository();
            StatusRepository Srepo = new StatusRepository();
            Crepo.GetCustomers().ForEach(e => ComboBoxCustomers.Items.Add(e.Naam));
            Srepo.GetAllStatus().ForEach(e => ComboBoxStatus.Items.Add(e.Betekenis));
        }
    }
}
