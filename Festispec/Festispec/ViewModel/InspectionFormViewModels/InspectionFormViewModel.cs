using Festispec.Model;
using Festispec.Model.Enums;
using Festispec.Model.Repositories;
using Festispec.Service;
using Festispec.Utility.Validators;
using Festispec.Validators;
using Festispec.ViewModel.InspectionFormViewModels.Interfaces;
using FluentValidation.Results;
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
        public ICommand AddTableQuestionCommand { get; set; }
        public ICommand ToShowModeCommand { get; set; }
        public ICommand QuestionUpCommand { get; set; }
        public ICommand QuestionDownCommand { get; set; }
        public ICommand DeleteQuestionCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        #endregion

        private Inspectieformulier _inspectionForm;

        private InspectionFormRepository _repo;

        private NavigationService _navigationService;

        private QuestionViewModel _selectedVraag;

        public bool Changed;

        public bool NewInspectionForm;

        private List<QuestionViewModel> _removedQuestions;

        public Inspectieformulier InspectionForm {
            get => _inspectionForm;
            set
            {
                _inspectionForm = value;
                RaisePropertyChanged("InspectionForm");
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

        #region InspectionFormVariable
        private String _titel;

        public string Titel
        {
            get {
                if (_titel == null)
                    _titel = _inspectionForm.InspectieFormulierTitel;

                return _titel;
            }
            set
            {
                _titel = value;
                _inspectionForm.InspectieFormulierTitel = _titel;
                Changed = true;
                TitelError = null;
                RaisePropertyChanged("Titel");
            }
        }

        private string _titelError;
        public string TitelError
        {
            get => _titelError;
            set
            {
                _titelError = value;
                RaisePropertyChanged("TitelError");
            }
        }

        private string _saveError;
        public string SaveError
        {
            get => _saveError;
            set
            {
                _saveError = value;
                RaisePropertyChanged("SaveError");
            }
        }

        private string _saveMessage;
        public string SaveMessage
        {
            get => _saveError;
            set
            {
                _saveError = value;
                RaisePropertyChanged("SaveMessage");
            }
        }

        private string _description;

        public string Description
        {
            get
            {
                if (_description == null)
                    _description = _inspectionForm.Beschrijving;

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
                _questions = new ObservableCollection<QuestionViewModel>(_questions.OrderBy(x => x.OrderNumber));
                return _questions;
            }
        }

        public InspectionFormViewModel(NavigationService nav, InspectionFormRepository repo)
        {//deze constructor wordt gebruikt om het vm te maken voor de vragenlijst
            _navigationService = nav;
            _repo = repo;
            if (nav.Parameter is Inspectieformulier)//als er een al bestaande inspectieformulier wordt meegegeven
            {
                InspectionForm = (Inspectieformulier)nav.Parameter;
                if (InspectionForm.InspectieformulierID == 0)
                {
                    LastChangeDate = DateTime.Now;
                    NewInspectionForm = true;
                    Save();
                }
            }
            else if(nav.Parameter is int){//als er geen inspectieformulier wordt meegegeven en een nieuwe moet worden aangemaakt.
                InspectionForm = new Inspectieformulier();
                Titel = "Nieuw formulier";
                Description = "Formulier beschrijving";
                _inspectionForm.OpdrachtID = (int)nav.Parameter;
                LastChangeDate = DateTime.Now;
                NewInspectionForm = true;
                Save();
            }
            else if(nav.Parameter is null)//dit is het geval bij het aanmaken van een template
            {
                InspectionForm = new Inspectieformulier();
                Titel = "Nieuw formulier template";
                Description = "Template beschrijving";
                LastChangeDate = DateTime.Now;
                NewInspectionForm = true;
                Save();
            }
            
            _removedQuestions = new List<QuestionViewModel>();
            CreateCommands();
        }

        public InspectionFormViewModel(NavigationService nav, InspectionFormRepository repo, Inspectieformulier form)
        {//deze constructor wordt gebruikt om de vms te maken die de vragenlijsten showen in de list
            _navigationService = nav;
            _repo = repo;
            InspectionForm = form;
            _removedQuestions = new List<QuestionViewModel>();
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
            AddTableQuestionCommand = new RelayCommand(AddTableQuestion);
            ToShowModeCommand = new RelayCommand(ToShowCommand);
            SaveCommand = new RelayCommand(Save);
            QuestionUpCommand = new RelayCommand(QuestionUp);
            QuestionDownCommand = new RelayCommand(QuestionDown);
            DeleteQuestionCommand = new RelayCommand(RemoveQuestion);

        }

        #region Commands
        public void ToEdit()
        {
            _navigationService.NavigateTo("InspectionFormEditView", this);
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

            AddQuestion(v);
        }

        public void AddTableQuestion()
        {
            Vraag v = new Vraag
            {
                Vraagtype = "tv",
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
                return;
            int index = Questions.IndexOf(SelectedQuestion);
            if (index == Questions.Count() - 1)
                return;
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
                return;
            int SelectedIndex = (SelectedQuestion.OrderNumber - 1);
            if (!(SelectedIndex == Questions.Count()))
                for (int i = SelectedIndex; i < Questions.Count(); i++)
                {
                    Questions[i].OrderNumber = Questions[i].OrderNumber - 1;
                }
            _removedQuestions.Add(SelectedQuestion);
            Questions.Remove(SelectedQuestion);
            if (SelectedIndex == Questions.Count())
                SelectedIndex = Questions.Count() - 1;
            if (Questions.Count() > 0)
                SelectedQuestion = Questions[SelectedIndex];
            else
                SelectedQuestion = null;
            
            Changed = true;
            RaisePropertyChanged("Questions");
        }

        public void ToShowCommand()
        {
            if (_inspectionForm.OpdrachtID == null)
                _navigationService.NavigateTo("InspectionFormTemplateOverview", EnumTemplateMode.EDIT);
            else
                _navigationService.NavigateTo("InspectionFormShowView", InspectionForm.OpdrachtID);
        }

        public void Save()
        {
            SaveError = null;
            SaveMessage = null;
            if (NewInspectionForm)
            {
                NewInspectionForm = false;
                _inspectionForm.InspectieformulierID = _repo.CreateInspectieFormulier(_inspectionForm);
                return;
            }

            bool valid = true;

            foreach (QuestionViewModel question in Questions)
            {
                question.Validate();
                if (!question.Validate())
                {
                    valid = false;
                }
            }
            TitelError = null;
            ValidationResult result = new InspectionFormValidator().Validate(this);
            if (!result.IsValid)
            {
                TitelError = result.Errors.FirstOrDefault().ErrorMessage;
                valid = false;
            }

            if (!valid)
            {
                SaveError = "Opslaan niet gelukt.";
                return;
            }

            if (Changed)
            {
                Changed = false;
                SaveMessage = "Opslaan gelukt";
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
                    question.Changed = false;
                    question.QuestionID = _repo.AddQuestion(question.Question);
                    if (question.QuestionType == "mv" || question.QuestionType == "tv")
                    {
                        List<VraagMogelijkAntwoord> newPosAnswers = new List<VraagMogelijkAntwoord>();
                        foreach (PossibleAnwserViewModel posAnwser in question.PossibleAnwsers)
                        {
                            newPosAnswers.Add(posAnwser.PossibleAnwser);
                        }
                        _repo.AddPossibleAnswers(newPosAnswers);
                        SaveMessage = "Opslaan gelukt";
                    }
                }
                else if (question.Changed)
                {
                    _repo.UpdateQuestion(question.Question);
                    question.Changed = false;
                    if (question.QuestionType == "sv" || question.QuestionType == "mv" || question.QuestionType == "tv")
                    {
                        List<VraagMogelijkAntwoord> newPosAnswers = new List<VraagMogelijkAntwoord>();
                        foreach (PossibleAnwserViewModel posAnwser in question.PossibleAnwsers)
                        {
                            newPosAnswers.Add(posAnwser.PossibleAnwser);
                        }
                        _repo.updatePossibleAnswers(newPosAnswers);
                        SaveMessage = "Opslaan gelukt";
                    }
                }
                
            }

            
        }

        public void SaveInspectionformDetails() => _repo.UpdateInspectieFormulier(InspectionForm);
        #endregion

        public void Swap(int index1, int index2)
        {
            QuestionViewModel memory1 = Questions[index1];
            QuestionViewModel memory2 = Questions[index2];
            memory1.OrderNumber = index2 + 1;
            memory2.OrderNumber = index1 + 1;
            RaisePropertyChanged("Questions");
        }

        public void AddQuestion(Vraag v)
        {
            QuestionViewModel newQ = new QuestionViewModel(v, _inspectionForm);
            Questions.Add(newQ);
            _inspectionForm.Vraag.Add(v);
            v.VolgordeNummer = Questions.Count();
            
            SelectedQuestion = newQ;
            Changed = true;
            newQ.Created = true;
            RaisePropertyChanged("Questions");
        }
    }
}
