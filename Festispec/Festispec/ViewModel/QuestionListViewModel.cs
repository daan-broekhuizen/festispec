using Festispec.Model;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel
{
    public class QuestionListViewModel : ViewModelBase
    {

        public List<Question> QList { get; set; }

        public QuestionListViewModel()
        {
            QList = new List<Question>();
            Question q = new OpenQuestion();
            q.QuestionText = "test";
            AddQuestion(q);

        }

        public void AddQuestion(Question q)
        {
            q.QuestionID = QList.Count + 1;
            QList.Add(q);
        }

        public List<Question> GetList
        {
            get { return QList;}      
        }

        public List<Question> GetMultipleChoiceQuestions
        {
            get { List<Question> q = new List<Question>();
                for (int i = 0; i < QList.Count; i++)
                {
                    if (QList[i] is MultipleChoiceQuestion)
                    {
                        q.Add(QList[i]);

                    }
                }

                return q;
            }
        }

        public List<Question> GetOpenQuestions
        {
            get
            {
                List<Question> q = new List<Question>();
                for (int i = 0; i < QList.Count; i++)
                {
                    if (QList[i] is OpenQuestion)
                    {
                        q.Add(QList[i]);

                    }
                }

                return q;
            }
        }



    }
}
