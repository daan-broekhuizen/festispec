using Festispec.Model;
using Festispec.Model.Repositories;
using Festispec.Service;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Festispec.Service;

namespace Festispec.ViewModel.InspectionFormViewModels
{
    public class InspectionFormListViewModel : ViewModelBase
    {
        public ObservableCollection<InspectionFormViewModel> InspectionFormsList { get; set; }

        public ICommand ToEditCommand { get; set; }
        public ICommand CreateNewInspectionFormCommand { get; set; }
        public ICommand SaveDetailsCommand { get; set; }
        public ICommand ToJobCommand { get; set; }

        private NavigationService _navigationService;

        private InspectionFormViewModel _selectedInspectionForm;

        private int _jobID;

        public InspectionFormViewModel SelectedInspectionForm
        {
            get => _selectedInspectionForm;
            set
            {
                _selectedInspectionForm = value;
                RaisePropertyChanged("SelectedInspectionForm");
                RaisePropertyChanged("Titel");
                RaisePropertyChanged("Description");
                RaisePropertyChanged("LastChangeDate");
                RaisePropertyChanged("Questions");
                RaisePropertyChanged("InspectionDate");
                RaisePropertyChanged("InspectionStartTime");
                RaisePropertyChanged("InspectionEndTime");
                RaisePropertyChanged("City");
                RaisePropertyChanged("Street");
                RaisePropertyChanged("HouseNumber");
                RaisePropertyChanged("RequiredInspectors");
            }
        }

        private InspectionFormRepository _repo;

        public InspectionFormListViewModel(NavigationService navigationService, InspectionFormRepository inspectionFormRepo)
        {
            this._navigationService = navigationService;
            this._repo = inspectionFormRepo;
            if (_navigationService.Parameter is int)
            {
                _jobID = (int)_navigationService.Parameter;
                InspectionFormsList = new ObservableCollection<InspectionFormViewModel>();
                GetInspectionForms();
            }
            CreateNewInspectionFormCommand = new RelayCommand(CreateNewInspectionForm);
            ToEditCommand = new RelayCommand(ToEditView);
            SaveDetailsCommand = new RelayCommand(SaveInspectionFormDetailsAsync);
            ToJobCommand = new RelayCommand(ToJobView);
        }

        private void GetInspectionForms()
        {
            List<Inspectieformulier> inspectionForms = new List<Inspectieformulier>(_repo.GetInspectieformulier(_jobID));
            
            if (inspectionForms.Count() != 0)
            {
                foreach(Inspectieformulier form in inspectionForms)
                {
                    InspectionFormViewModel formVM = new InspectionFormViewModel(_navigationService, _repo, form);

                    InspectionFormsList.Add(formVM);
                }
                SelectedInspectionForm = InspectionFormsList.FirstOrDefault();
            }
        }

        private async Task<bool> ValidateAdressAsync(string addres)
        {
            LocationService service = new LocationService();
            return await service.CalculateDistance(addres, "Griekenland") != 0.0;
        }

        #region inspectionFormVariables
        public string Titel
        {
            get
            {
                if(_selectedInspectionForm != null){return _selectedInspectionForm.Titel;}
                else{return null;}
            }
        }

        public DateTime? InspectionDate
        {
            get => _selectedInspectionForm.InspectionForm.DatumInspectie;
            set
            {
                _selectedInspectionForm.InspectionForm.DatumInspectie = value;
                RaisePropertyChanged("InspectionDate");
            }
        }

        public DateTime InspectionStartTime
        {
            get
            {
                TimeSpan t = _selectedInspectionForm.InspectionForm.StartTijd ?? new TimeSpan(0,0,0);
                DateTime dt = new DateTime() + t;
                return dt;
            }
            set
            {
                _selectedInspectionForm.InspectionForm.StartTijd = value.TimeOfDay;
                RaisePropertyChanged("InspectionStartTime");
                RaisePropertyChanged("MinimumTime");
            }
        }

        public DateTime InspectionEndTime
        {
            get
            {
                TimeSpan t = _selectedInspectionForm.InspectionForm.EindTijd ?? new TimeSpan(23, 0, 0);
                DateTime dt = new DateTime() + t;
                return dt;
            }
            set
            {
                _selectedInspectionForm.InspectionForm.EindTijd = value.TimeOfDay;
                RaisePropertyChanged("InspectionEndTime");
                RaisePropertyChanged("MaximumTime");
            }
        }

        public DateTime MinimumTime
        {
            get => InspectionStartTime.AddHours(1);      
        }

        public DateTime MaximumTime
        {
            get => InspectionEndTime.AddHours(-1);
        }

        public string City
        {
            get => _selectedInspectionForm.InspectionForm.Stad;
            set
            {
                _selectedInspectionForm.InspectionForm.Stad = value;
                RaisePropertyChanged("City");
            }
        }

        public string Street
        {
            get => _selectedInspectionForm.InspectionForm.Straatnaam;
            set
            {
                _selectedInspectionForm.InspectionForm.Straatnaam = value;
                RaisePropertyChanged("Street");
            }
        }

        public string HouseNumber
        {
            get => _selectedInspectionForm.InspectionForm.Huisnummer;
            set
            {
                _selectedInspectionForm.InspectionForm.Huisnummer = value;
                RaisePropertyChanged("HouseNumber");
            }
        }

        public int? RequiredInspectors
        {
            get => _selectedInspectionForm.InspectionForm.BenodigdeInspecteurs;
            set
            {
                _selectedInspectionForm.InspectionForm.BenodigdeInspecteurs = value;
                RaisePropertyChanged("RequiredInspectors");
            }
        }

        public DateTime MinimalDate
        {
            get => DateTime.Today.AddDays(2);
            set
            { return; }
        }

        public DateTime MaximumDate
        {
            get => DateTime.Today.AddYears(1).AddMonths(6);
            set{ return; }
        }

        public string Description
        {
            get
            {
                if (_selectedInspectionForm != null)
                { return _selectedInspectionForm.Description; }
                else { return null; }
            }
        }

        public DateTime LastChangeDate
        {
            get
            {
                if (_selectedInspectionForm != null){ return _selectedInspectionForm.LastChangeDate; }
                else { return DateTime.Now; }
            }
        }

        public ObservableCollection<QuestionViewModel> Questions
        {
            get
            {
                if (_selectedInspectionForm != null){ return _selectedInspectionForm.Questions; }
                else { return null; }
            }
        }
        #endregion

        public void CreateNewInspectionForm() => _navigationService.NavigateTo("InspectionFormTemplateOverview", _jobID);

        public void ToEditView() => _navigationService.NavigateTo("InspectionFormEditView", _selectedInspectionForm.InspectionForm);

        public void ToJobView()
        {
            JobViewModel jvm = new JobViewModel(_repo.GetJob(_jobID));
            _navigationService.NavigateTo("JobInfo", jvm);
        }
        public async void SaveInspectionFormDetailsAsync()
        {
            if(Street == null || HouseNumber == null || City == null)
            {
                if(_selectedInspectionForm != null)
                    _selectedInspectionForm.SaveInspectionformDetails();
            }
            else
            {
                string address = Street + " " + HouseNumber + " " + City;
                if (await ValidateAdressAsync(address))//als adres niet klopt crasht dit. Navragen aan Mike
                {
                if (_selectedInspectionForm != null)
                    _selectedInspectionForm.SaveInspectionformDetails();
                }
            }
            
        }
    }
}
