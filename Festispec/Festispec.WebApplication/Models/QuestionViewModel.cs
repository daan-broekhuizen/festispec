using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Festispec.WebApplication.Models
{
    public class QuestionViewModel
    {
        public Vraag Question { get; set; }
        public Antwoorden Answer { get; set; }
        public string AnswerText { get; set; }
        public byte[] AnswerImage { get; set; }
    }
}