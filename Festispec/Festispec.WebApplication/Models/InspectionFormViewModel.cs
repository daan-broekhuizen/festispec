﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Festispec.WebApplication.Models
{
    public class InspectionformViewModel
    {
        public Inspectieformulier Inspectionform { get; set; }
        public List<QuestionViewModel> Questions { get; set; }
        public string LogoString => Inspectionform.Opdracht.Klant.Klant_logo != null ? Convert.ToBase64String(Inspectionform.Opdracht.Klant.Klant_logo) : null;
        public int CompletedQuestions { get; set; }
        public bool IsEditable => DateTime.Now.Date == Inspectionform.DatumInspectie;
        
        
    }
}