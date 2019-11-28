using Festispec.Model;
using Festispec.Model.Repositories;
using Festispec.Service;
using Festispec.ViewModel.InspectionFormViewModels.Interfaces;
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
    public class InspectionFormViewModel : ViewModelBase, IInspectionFormViewModel
    {
        #region ICommands
        public ICommand ToEditModeCommand { get; set; }
        public ICommand AddOpenQuestionCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        #endregion

        private Inspectieformulier _inspectionForm;

        private InspectionFormRepository _repo;

        private NavigationService _navigationService;

        private bool _editMode;

        public Inspectieformulier InspectionForm {
            get => _inspectionForm;
            set {
                _inspectionForm = value;
            }
        }

        public bool EditMode
        {
            get => _editMode;
            set
            {
                _editMode = value;
                RaisePropertyChanged("EditMode");
            }
        }

        #region InspectionFormVariable
        private String _titel;

        public string Titel
        {
            get => _titel;
            set
            {
                _titel = value;
                _inspectionForm.InspectieFormulierTitel = _titel;
                RaisePropertyChanged("Titel");
            }
        }

        

        #endregion

        private ObservableCollection<Vraag> _questions;
        public ObservableCollection<Vraag> Questions
        {
            get
            {
                if (_questions == null)
                {
                    _questions = new ObservableCollection<Vraag>();
                    foreach (var voc in _inspectionForm.InspectieformulierVragenlijstCombinatie)
                    {
                        _questions.Add(voc.Vraag);
                    }
                }
  
                return _questions;
            }
        }

        public InspectionFormViewModel(NavigationService nav, InspectionFormRepository repo)
        {
            _navigationService = nav;
                //navragen
            if (nav.Parameter is Inspectieformulier)
            {
                _inspectionForm = (Inspectieformulier)nav.Parameter;
            }

            //TO TEST MOET WORDEN WEGGEHAALD IN EINDVERSIE
            InspectionForm = new Inspectieformulier();
            Titel = "test";

            _repo = repo;

            CreateCommands();
        }

        private void CreateCommands()
        {
            ToEditModeCommand = new RelayCommand(ToEdit);
            AddOpenQuestionCommand = new RelayCommand(AddOpenQuestion);
            SaveCommand = new RelayCommand(Save);
        }

        #region Commands
        public void ToEdit()
        {
            if (EditMode == true)
            {
                EditMode = false;
            }
            else
            {
                EditMode = true;
            }
        }

        public void AddOpenQuestion()
        {
            Vraag newQ = new Vraag
            {
                Vraagtype = "ov",
                Vraagstelling= "test"
            };

            Questions.Add(newQ);
            RaisePropertyChanged("Questions");

        }
        #endregion

        public void Save()
        {
            //to DB
            //Laatse weiziging = cur date
            //Delete all voc and create new / update
        }
    }
}
