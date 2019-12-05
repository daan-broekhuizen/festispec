using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Festispec.Model.Repositories;
using FestiSpec.Domain.Repositories;
using GalaSoft.MvvmLight;

namespace Festispec.ViewModel
{
    public class ManagementViewModel : ViewModelBase
    {
        #region Properties
        public JobRepository _jrepo { get; set; }
        public QuotationRepository _qrepo { get; set; }
        public UserRepository _urepo { get; set; }
        public CustomerRepository _crepo { get; set; }

        private DateTime _startDate;

        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                _startDate = value;
                RaisePropertyChanged("StartDate");
            }
        }

        public DateTime EndDate { get; set; }
        private ComboBoxItem _selectedItem;

        public ComboBoxItem SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                RaisePropertyChanged("SelectedItem");
            }
        }
        #endregion

        public ManagementViewModel(JobRepository Jrepo, QuotationRepository Qrepo, UserRepository Urepo, CustomerRepository Crepo)
        {
            OfferteGraphViewModel offerteGraph = new OfferteGraphViewModel();
        }

        public ManagementViewModel()
        {

        }
    }
}
