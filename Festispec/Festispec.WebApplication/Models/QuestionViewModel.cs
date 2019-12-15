﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Festispec.WebApplication.Models
{
    public class QuestionViewModel
    {
        public Vraag Question { get; set; }
        public Antwoorden Answer { get; set; }
        public string AnswerText { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
        public bool IsAnswered { get; set; }

        public IEnumerable<SelectListItem> PossibleAnswers
        {
            get => new SelectList(Question.VraagMogelijkAntwoord, "AntwoordText", "AntwoordText");
        }
    }
}