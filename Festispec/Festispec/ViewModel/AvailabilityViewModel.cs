using Festispec.Model;
using Festispec.Model.Repositories;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Festispec.ViewModel
{
    public class AvailabilityViewModel : ViewModelBase
    {
        private AvailabilityRepository _repo;
        private AvailabilityModel newAvailability;
        private Nullable<DateTime> selectedDate { get; set; }

        public ObservableCollection<AvailabilityModel> Availabilities { get; set; }
        public ICommand AddAvailabilityCommand { get; set; }
        public AvailabilityViewModel()
        {
            this._repo = new AvailabilityRepository();
            this.Availabilities = this._repo.GetAvailabilities();
            this.AddAvailabilityCommand = new RelayCommand(AddAvailability);
        }

        public Nullable<DateTime> SelectedDate
        {
            get { return selectedDate; }
            set
            {
                if (selectedDate != value) { /* SelectedDate has changed */}
                selectedDate = value;
                RaisePropertyChanged();
            }
        }

        public void AddAvailability()
        {
            if(selectedDate != null)
            {
                newAvailability = new AvailabilityModel(selectedDate.Value);
                this._repo._availabilities.Add(newAvailability);
                RaisePropertyChanged();
            }
        }

        
    }
}
