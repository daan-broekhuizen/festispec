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

        public bool Changed;

        public bool NewInspectionForm;

        private List<QuestionViewModel> _removedQuestions;

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
                Changed = true;
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
                Changed = true;
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
                Changed = true;
                RaisePropertyChanged("Description");
            }
        }

        public DateTime LastChangeDate
        {
            get => _inspectionForm.LaatsteWijziging;
            set
            {
                _inspectionForm.LaatsteWijziging = value;
                RaisePropertyChanged("LastChangeDate");
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
                    foreach (Vraag question in _inspectionForm.Vraag)
                    {
                        _questions.Add(new QuestionViewModel(question, _inspectionForm)); ;
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
            else{//test
                InspectionForm = new Inspectieformulier();
                Titel = "Nieuw formulier";
                Description = "Formulier beschrijving";
                _inspectionForm.OpdrachtID = 1;
                LastChangeDate = DateTime.Now;
                NewInspectionForm = true;
            }
  
            _repo = repo;
            _removedQuestions = new List<QuestionViewModel>();
            CreateCommands();
            Save();
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
            AddQuestion(v);
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

            AddQuestion(v);
        }

        public void AddText()
        {
            Vraag v = new Vraag
            {
                Vraagtype = "tx",
            };

            AddQuestion(v);
        }

        public void AddPictureQuestion()
        {
            Vraag v = new Vraag
            {
                Vraagtype = "av",
            };

            AddQuestion(v);
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

            AddQuestion(v);
        }

        public void AddTable2Question()
        {
            Vraag v = new Vraag
            {
                Vraagtype = "t2",
            };

            AddQuestion(v);
        }

        public void AddTable3Question()
        {
            Vraag v = new Vraag
            {
                Vraagtype = "t3",
            };

            AddQuestion(v);
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
                Changed = true;
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
                Changed = true;
                RaisePropertyChanged("Questions");
            }
        }

        public void RemoveQuestion()
        {
            if (SelectedQuestion == null)
            {
                return;
            }
            int SelectedIndex = (SelectedQuestion.OrderNumber - 1);
            if (!(SelectedIndex == Questions.Count()))
            {
                for (int i = SelectedIndex; i < Questions.Count(); i++)
                {
                    Questions[i].OrderNumber = Questions[i].OrderNumber - 1;
                }
            }
            _removedQuestions.Add(SelectedQuestion);
            Questions.Remove(SelectedQuestion);
            if (SelectedIndex == Questions.Count())
            {
                SelectedIndex = Questions.Count() - 1;
            }
            if (Questions.Count() > 0)
            {
                SelectedQuestion = Questions[SelectedIndex];
            }
            else
            {
                SelectedQuestion = null;
            }
            
            Changed = true;
            RaisePropertyChanged("Questions");
        }

        public void ToShowCommand()
        {
            //todo
        }

        public void Save()
        {
            if (NewInspectionForm)
            {
                NewInspectionForm = false;
                _inspectionForm.InspectieformulierID = _repo.CreateInspectieFormulier(_inspectionForm);
                return;
            }

            if (Changed)
            {
                Changed = false;
                LastChangeDate = DateTime.Now;
                _repo.UpdateInspectieFormulier(_inspectionForm);

                foreach (QuestionViewModel question in _removedQuestions)
                {
                    _repo.RemoveQuestionFromInspectionForm(question.Question);
                }

                _removedQuestions.Clear();   
            }

            foreach (QuestionViewModel question in _questions)
            {

                if (question.Created)
                {
                    question.Created = false;
                    question.QuestionID = _repo.AddQuestion(question.Question);
                }
                else if (question.Changed)
                {
                    _repo.UpdateQuestion(question.Question);
                    question.Changed = false;
                }

            }
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

        public void AddQuestion(Vraag v)
        {
            QuestionViewModel newQ = new QuestionViewModel(v, _inspectionForm);
            Questions.Add(newQ);
            _inspectionForm.Vraag.Add(v);
            v.VolgordeNummer = _questions.Count();
            
            SelectedQuestion = newQ;
            Changed = true;
            newQ.Created = true;
            RaisePropertyChanged("Questions");
        }
    }
}
