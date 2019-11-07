using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Festispec.View;

namespace Festispec.ViewModel
{
    public class QuestionEditVM : ViewModelBase
    {
        private QuestionListViewModel _questionList;
        public ICommand AddQuestionCommand { get; set; }
        public QuestionViewModel Question { get; set; }

        public QuestionEditVM(QuestionListViewModel questionList)
        {
            this._questionList = questionList;
            this.Question = new QuestionViewModel();
            this.AddQuestionCommand = new RelayCommand(AddQuestion);
        }

        public void AddQuestion()
        {
            if (Question.QuestionText == null)
            {
                Debug.WriteLine("Vraag niet toegevoegd");
                return;
            }
            _questionList.AddQuestion(Question.GetQuestion());
            Debug.WriteLine("Vraag toegevoegd");
            
        }
    }
}
