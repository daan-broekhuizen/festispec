using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace Festispec.WebApplication.Models
{
    public class QuestionViewModel
    {
        public Vraag Question { get; set; }
        public Antwoorden Answer { get; set; }
        public string AnswerText { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
        public bool IsAnswered { get; set; }
        public int QuestionId { get; set; }
        public List<TableAnswerViewModel> TableAnswers { get; set; }

        public IEnumerable<SelectListItem> PossibleAnswers
        {
            get => new SelectList(Question.VraagMogelijkAntwoord, "AntwoordText", "AntwoordText");
        }
    }
}