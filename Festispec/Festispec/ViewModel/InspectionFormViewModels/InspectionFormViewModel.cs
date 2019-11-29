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
        public ICommand AddMultipleChoiceQuestionCommand { get; set; }
        public ICommand AddTextCommand { get; set; }
        public ICommand AddPictureQuestionCommand { get; set; }
        public ICommand AddScaleQuestionCommand { get; set; }
        public ICommand AddTable2QuestionCommand { get; set; }
        public ICommand AddTable3QuestionCommand { get; set; }
        public ICommand ToShowModeCommand { get; set; }
        public ICommand QuestionUpCommand { get; set; }
        public ICommand QuestionDownCommand { get; set; }
        public ICommand DeleteQuestionCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        #endregion

        private Inspectieformulier _inspectionForm;

        private InspectionFormRepository _repo;

        private NavigationService _navigationService;

        private bool _editMode;

        private QuestionViewModel _selectedVraag;

        public Inspectieformulier InspectionForm {
            get => _inspectionForm;
            set {
                _inspectionForm = value;
            }
        }

        public QuestionViewModel SelectedQuestion
        {
            get => _selectedVraag;
            set
            {
                _selectedVraag = value;
                RaisePropertyChanged("SelectedVraag");
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
            get {
                if (_titel == null)
                {
                    _titel = _inspectionForm.InspectieFormulierTitel;
                }

                return _titel;
            }
            set
            {
                _titel = value;
                _inspectionForm.InspectieFormulierTitel = _titel;
                RaisePropertyChanged("Titel");
            }
        }

        private string _description;

        public string Description
        {
            get
            {
                if (_description == null)
                {
                    _description = _inspectionForm.Beschrijving;
                }

                return _description;
            }
            set
            {
                _description = value;
                _inspectionForm.Beschrijving = _description;
                RaisePropertyChanged("Description");
            }
        }

        

        #endregion

        private ObservableCollection<QuestionViewModel> _questions;
        public ObservableCollection<QuestionViewModel> Questions
        {
            get
            {
                if (_questions == null)
                {
                    _questions = new ObservableCollection<QuestionViewModel>();
                    foreach (var voc in _inspectionForm.InspectieformulierVragenlijstCombinatie)
                    {
                        _questions.Add(new QuestionViewModel(voc.Vraag, _inspectionForm.InspectieformulierID)); ;
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
            AddMultipleChoiceQuestionCommand = new RelayCommand(AddMultipleChoiceQuestion);
            AddTextCommand = new RelayCommand(AddText);
            AddPictureQuestionCommand = new RelayCommand(AddPictureQuestion);
            AddScaleQuestionCommand = new RelayCommand(AddScaleQuestion);
            AddTable2QuestionCommand = new RelayCommand(AddTable2Question);
            AddTable3QuestionCommand = new RelayCommand(AddTable3Question);
            ToShowModeCommand = new RelayCommand(ToShowCommand);
            SaveCommand = new RelayCommand(Save);
            QuestionUpCommand = new RelayCommand(QuestionUp);
            QuestionDownCommand = new RelayCommand(QuestionDown);
            DeleteQuestionCommand = new RelayCommand(RemoveQuestion);

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
            Vraag v = new Vraag
            {
                Vraagtype = "ov",
            };

            QuestionViewModel newQ = new QuestionViewModel(v, _inspectionForm.InspectieformulierID);
            AddNewVIC(newQ);
            Questions.Add(newQ);
            SelectedQuestion = newQ;
            RaisePropertyChanged("Questions");

        }

        public void AddMultipleChoiceQuestion()
        {
            Vraag v = new Vraag
            {
                Vraagtype = "mv",
            };

            for (int i = 0; i < 4; i++)
            {
                v.VraagMogelijkAntwoord.Add(new VraagMogelijkAntwoord
                {
                    AntwoordNummer = i + 1,
                    Vraag = v
                }) ;
            }

            QuestionViewModel newQ = new QuestionViewModel(v, _inspectionForm.InspectieformulierID);
            AddNewVIC(newQ);
            Questions.Add(newQ);
            SelectedQuestion = newQ;
            RaisePropertyChanged("Questions");
        }

        public void AddText()
        {
            Vraag v = new Vraag
            {
                Vraagtype = "tx",
            };

            QuestionViewModel newQ = new QuestionViewModel(v, _inspectionForm.InspectieformulierID);
            AddNewVIC(newQ);
            Questions.Add(newQ);
            SelectedQuestion = newQ;
            RaisePropertyChanged("Questions");
        }

        public void AddPictureQuestion()
        {
            Vraag v = new Vraag
            {
                Vraagtype = "av",
            };

            QuestionViewModel newQ = new QuestionViewModel(v, _inspectionForm.InspectieformulierID);
            AddNewVIC(newQ);
            Questions.Add(newQ);
            SelectedQuestion = newQ;
            RaisePropertyChanged("Questions");
        }

        public void AddScaleQuestion()
        {
            Vraag v = new Vraag
            {
                Vraagtype = "sv",
            };

            for (int i = 0; i < 5; i++)
            {
                v.VraagMogelijkAntwoord.Add(new VraagMogelijkAntwoord
                {
                    AntwoordNummer = i + 1,
                    Vraag = v,
                    AntwoordText = (i + 1).ToString()
                });
            }

            QuestionViewModel newQ = new QuestionViewModel(v, _inspectionForm.InspectieformulierID);
            AddNewVIC(newQ);
            Questions.Add(newQ);
            SelectedQuestion = newQ;
            RaisePropertyChanged("Questions");
        }

        public void AddTable2Question()
        {
            Vraag v = new Vraag
            {
                Vraagtype = "t2",
            };

            QuestionViewModel newQ = new QuestionViewModel(v, _inspectionForm.InspectieformulierID);
            AddNewVIC(newQ);
            Questions.Add(newQ);
            SelectedQuestion = newQ;
            RaisePropertyChanged("Questions");
        }

        public void AddTable3Question()
        {
            Vraag v = new Vraag
            {
                Vraagtype = "t3",
            };

            QuestionViewModel newQ = new QuestionViewModel(v, _inspectionForm.InspectieformulierID);
            AddNewVIC(newQ);
            Questions.Add(newQ);
            SelectedQuestion = newQ;
            RaisePropertyChanged("Questions");
        }

        public void QuestionUp()
        {
            if (SelectedQuestion == null)
            {
                return;
            }
            int index = Questions.IndexOf(SelectedQuestion);
            if (index == 0)
            {
                return;
            }
            else
            {
                Swap(index, index - 1);
                RaisePropertyChanged("Questions");
            }
        }

        public void QuestionDown()
        {
            if(SelectedQuestion == null)
            {
                return;
            }
            int index = Questions.IndexOf(SelectedQuestion);
            if (index == Questions.Count() - 1)
            {
                return;
            }
            else
            {
                Swap(index, index + 1);
                RaisePropertyChanged("Questions");
            }
        }

        public void RemoveQuestion()
        {
            if (SelectedQuestion == null)
            {
                return;
            }
            for (int i = Questions.IndexOf(SelectedQuestion) + 1; i < Questions.Count(); i++)
            {
                Questions[i].OrderNumber = Questions[i].OrderNumber - 1;
            }
            Questions.Remove(SelectedQuestion);
            SelectedQuestion = null;
            RaisePropertyChanged("Questions");
        }

        public void ToShowCommand()
        {
            //todo
        }

        public void Save()
        {
            //to DB
            //Laatse weiziging = cur date
            //Delete all voc and create new / update
        }
        #endregion

        public void Swap(int index1, int index2)
        {
            QuestionViewModel memory = Questions[index1];
            Questions[index1] = Questions[index2];
            Questions[index2] = memory;
            SelectedQuestion = Questions[index2];
            Questions[index1].OrderNumber = index1 + 1;
            Questions[index2].OrderNumber = index2 + 1;
        }
        
        public void AddNewVIC(QuestionViewModel q)
        {
            q.VIC.Add(new InspectieformulierVragenlijstCombinatie
            {
                Inspectieformulier = _inspectionForm,
                InspectieformulierID = _inspectionForm.InspectieformulierID,
                Vraag = q.Question,
                VraagID = q.QuestionID,
                VraagVolgordeNummer = _questions.Count() + 1,
            }
                );
        }
    }
}
