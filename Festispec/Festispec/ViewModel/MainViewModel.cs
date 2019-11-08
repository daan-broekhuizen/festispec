using Festispec.View;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.IO;
using System.Reflection;
using System.Windows.Input;

namespace Festispec.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public ICommand OpenGraphCommand { get; set; }
        private GraphView _graphView;

        public ICommand OpenQuestoinEditCommand { get; set; }
        private QuestionEditView _questionEditView;

        public ICommand OpenQuestionListCommand { get; set; }
        private QuestionListView _questionListView;

        public ICommand OpenMultipleChoiceQuestoinEditCommand { get; set; }
        private QuestionMultipleChoiceEditView _questionMultipleChoicEditView;
        public ICommand OpenAvailabilityCommand { get; set; }
        private AvailabilityView _availabilityView;

        public MainViewModel()
        {
            this.OpenGraphCommand = new RelayCommand(() => {
                this._graphView = new GraphView();
                this._graphView.Show();
            });

            this.OpenQuestoinEditCommand = new RelayCommand(() =>
            {
                this._questionEditView = new QuestionEditView();
                this._questionEditView.Show();
            });

            this.OpenQuestionListCommand = new RelayCommand(() =>
            {
                this._questionListView = new QuestionListView();
                this._questionListView.Show();
            });

            this.OpenMultipleChoiceQuestoinEditCommand = new RelayCommand(() =>
            {
                this._questionMultipleChoicEditView = new QuestionMultipleChoiceEditView();
                this._questionMultipleChoicEditView.Show();
            });

            this.OpenAvailabilityCommand = new RelayCommand(() =>
            {
                this._availabilityView = new AvailabilityView();
                this._availabilityView.Show();
            });
        }
    }
}