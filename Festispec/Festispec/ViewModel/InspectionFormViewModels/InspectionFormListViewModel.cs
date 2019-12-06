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

namespace Festispec.ViewModel.InspectionFormViewModels
{
    public class InspectionFormListViewModel : ViewModelBase
    {
        public ObservableCollection<InspectionFormViewModel> InspectionFormsList { get; set; }

        public ICommand ToEditCommand { get; set; }

        public ICommand CreateNewInspectionFormCommand { get; set; }

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

        #region inspectionFormVariables
        public string Titel
        {
            get
            {
                if(_selectedInspectionForm != null){return _selectedInspectionForm.Titel;}
                else{return null;}
            }
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

        public void CreateNewInspectionForm()
        {
            _navigationService.NavigateTo("InspectionFormEditView", _jobID);
        }

        public void ToEditView()
        {
            _navigationService.NavigateTo("InspectionFormEditView", _selectedInspectionForm.InspectionForm);
        }
    }
}
