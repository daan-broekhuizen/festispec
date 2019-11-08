using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Festispec.ViewModel
{
    public class MultipleChoiceQuestionEditViewModel
    {
        private QuestionListViewModel _questionList;
        public ICommand AddQuestionCommand { get; set; }
        public MultipleChoiceQuestionViewModel Question { get; set; }

        public String PosAnswerA { get; set; }
        public String PosAnswerB { get; set; }
        public String PosAnswerC { get; set; }
        public String PosAnswerD { get; set; }

        public MultipleChoiceQuestionEditViewModel(QuestionListViewModel questionList)
        {
            this._questionList = questionList;
            this.Question = new MultipleChoiceQuestionViewModel();
            this.AddQuestionCommand = new RelayCommand(AddQuestion);
        }

        public void AddQuestion()
        {
            
            if (PosAnswerA != null)
            {
                Question.AddPosAnswer(PosAnswerA);
            }
            if (PosAnswerB != null)
            {
                Question.AddPosAnswer(PosAnswerB);
            }
            if (PosAnswerC != null)
            {
                Question.AddPosAnswer(PosAnswerC);
            }
            if (PosAnswerD != null)
            {
                Question.AddPosAnswer(PosAnswerD);
            }
            if (Question.QuestionText == null || Question.getChoicesCount == 0)
            {
                Debug.WriteLine("Vraag niet toegevoegd");
                return;
            }
            _questionList.AddQuestion(Question.GetQuestion());
            Debug.WriteLine("Vraag toegevoegd");

        }
    }
}
