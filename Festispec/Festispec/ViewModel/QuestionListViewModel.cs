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

    }
}
