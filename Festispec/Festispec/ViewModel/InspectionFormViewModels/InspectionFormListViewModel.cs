using Festispec.Model;
using Festispec.Model.Repositories;
using Festispec.Service;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Festispec.Model.Enums;
using BingMapsRESTToolkit;

namespace Festispec.ViewModel.InspectionFormViewModels
{
    public class InspectionFormListViewModel : ViewModelBase
    {
        public ObservableCollection<InspectionFormViewModel> InspectionFormsList { get; set; }

        public ICommand ToEditCommand { get; set; }
        public ICommand CreateNewInspectionFormCommand { get; set; }
        public ICommand SaveDetailsCommand { get; set; }
        public ICommand ToJobCommand { get; set; }
        public ICommand DeleteInspectionFormCommand { get; set; }
        public ICommand GenerateScheduleCommand { get; set; }

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
                RaisePropertyChanged("Title");
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
            DeleteInspectionFormCommand = new RelayCommand(DeleteInspectionForm);
            GenerateScheduleCommand = new RelayCommand(GenerateScheduleAsync);
        }

        public async void GenerateScheduleAsync()
        {
            if (City != null && Street != null && HouseNumber != null && RequiredInspectors != null)
            {
                PlanningViewModel pvm = new PlanningViewModel();
                int ri = RequiredInspectors ?? default(int);

                string street = Street.Remove(Street.Length - 1, 1);
                string query = $"{street} {HouseNumber} {City}";
                try
                {
                    Address address = await new LocationService().GetFullAdress(query);
                    if (address.AddressLine.ToLower().Contains(Street.ToLower()))
                    {
                        if (await pvm.GetInspectorAsync(_selectedInspectionForm.InspectionForm.InspectieformulierID, City + " " + Street + " " + HouseNumber, ri) == null)
                            Messenger.Default.Send($"Planning kan niet gegenereerd worden.\n Er zijn te weinig beschikbare inspecteurs", this.GetHashCode());
                        else
                            Messenger.Default.Send($"Planning gegenereerd", this.GetHashCode());
                    }
                }
                catch
                {
                    Messenger.Default.Send($"Planning kan niet gegenereerd worden.\n Fout adres ingevuld", this.GetHashCode());
                }
            }
            
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

        public void DeleteInspectionForm()
        {
            if(SelectedInspectionForm != null)
            {
                _repo.DeleteInspectieFormulier(_selectedInspectionForm.InspectionForm);
                InspectionFormsList.Remove(_selectedInspectionForm);
                if(InspectionFormsList.Count() > 0)
                    _selectedInspectionForm = InspectionFormsList.FirstOrDefault();
                else
                    _selectedInspectionForm = null;
            }
        }

        #region inspectionFormVariables
        public string Title
        {
            get
            {
                if(_selectedInspectionForm != null)
                    return _selectedInspectionForm.Titel;
                else
                    return null;
            }
        }

        public DateTime? InspectionDate
        {
            get
            {
                if (_selectedInspectionForm != null)
                    return _selectedInspectionForm.InspectionForm.DatumInspectie;
                else
                    return null;
            }
            set
            {
                if (_selectedInspectionForm == null)
                    return;
                _selectedInspectionForm.InspectionForm.DatumInspectie = value;
                RaisePropertyChanged("InspectionDate");
            }
        }

        public DateTime InspectionStartTime
        {
            get
            {
                if (_selectedInspectionForm == null)
                    return new DateTime() + new TimeSpan(0,0,0); 
                TimeSpan t = _selectedInspectionForm.InspectionForm.StartTijd ?? new TimeSpan(0,0,0);
                DateTime dt = new DateTime() + t;
                return dt;
            }
            set
            {
                if (_selectedInspectionForm == null)
                    return;
                _selectedInspectionForm.InspectionForm.StartTijd = value.TimeOfDay;
                RaisePropertyChanged("InspectionStartTime");
            }
        }

        public DateTime InspectionEndTime
        {
            get
            {
                if (_selectedInspectionForm == null)
                    return new DateTime() + new TimeSpan(23,0,0);
                TimeSpan t = _selectedInspectionForm.InspectionForm.EindTijd ?? new TimeSpan(23, 0, 0);
                DateTime dt = new DateTime() + t;
                return dt;
            }
            set
            {
                if (_selectedInspectionForm == null)
                    return;
                _selectedInspectionForm.InspectionForm.EindTijd = value.TimeOfDay;
                RaisePropertyChanged("InspectionEndTime");
            }
        }

        public string City
        {
            get
            {
                if(_selectedInspectionForm == null)
                    return null;
                return _selectedInspectionForm.InspectionForm.Stad;
            }
            set
            {
                if (_selectedInspectionForm == null)
                    return;
                _selectedInspectionForm.InspectionForm.Stad = value;
                RaisePropertyChanged("City");
            }
        }

        public string Street
        {
            get
            {
                if (_selectedInspectionForm == null)
                    return null;
                return _selectedInspectionForm.InspectionForm.Straatnaam;
            }
            set
            {
                if (_selectedInspectionForm == null)
                    return;
                _selectedInspectionForm.InspectionForm.Straatnaam = value;
                RaisePropertyChanged("Street");
            }
        }

        public string HouseNumber
        {
            get
            {
                if (_selectedInspectionForm == null)
                    return null;
                return _selectedInspectionForm.InspectionForm.Huisnummer;
            }
            set
            {
                if (_selectedInspectionForm == null)
                    return;
                _selectedInspectionForm.InspectionForm.Huisnummer = value;
                RaisePropertyChanged("HouseNumber");
            }
        }

        public int? RequiredInspectors
        {
            get
            {
                if (_selectedInspectionForm == null)
                    return null;
                return _selectedInspectionForm.InspectionForm.BenodigdeInspecteurs;
            }
            set
            {
                if (_selectedInspectionForm == null)
                    return; 
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
            set
            { return; }
        }

        public string Description
        {
            get
            {
                if (_selectedInspectionForm != null)
                    return _selectedInspectionForm.Description;
                else
                    return null;
            }
        }

        public DateTime LastChangeDate
        {
            get
            {
                if (_selectedInspectionForm != null)
                    return _selectedInspectionForm.LastChangeDate;
                else
                    return DateTime.Now;
            }
        }

        public ObservableCollection<QuestionViewModel> Questions
        {
            get
            {
                if (_selectedInspectionForm != null)
                    return _selectedInspectionForm.Questions;
                else
                    return null;
            }
        }
        #endregion

        public void CreateNewInspectionForm() => _navigationService.NavigateTo("InspectionFormTemplateOverview", new object[2] { EnumTemplateMode.SELECT, _jobID });

        public void ToEditView()
        {
            if (_selectedInspectionForm == null)
                return;
            _navigationService.NavigateTo("InspectionFormEditView", _selectedInspectionForm.InspectionForm);
        }

        public void ToJobView()
        {
            JobViewModel jvm = new JobViewModel(_repo.GetJob(_jobID));
            _navigationService.NavigateTo("JobInfo", jvm);
        }
        public void SaveInspectionFormDetailsAsync()
        {
            if (_selectedInspectionForm != null)
            {
                _selectedInspectionForm.SaveInspectionformDetails();
                Messenger.Default.Send("Inspectiedetails opgeslagen", this.GetHashCode());
            }
                
        }
    }
}
