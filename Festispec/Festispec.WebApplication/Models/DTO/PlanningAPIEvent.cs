using Festispec.WebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Festispec.WebApplication.Models.DTO
{
    public class PlanningAPIEvent
    {
        public int id { get; set; }
        public string text { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }

        public static explicit operator PlanningAPIEvent(Inspectieformulier inspectie)
        {
            DateTime startdate = inspectie.DatumInspectie
                .GetValueOrDefault()
                .Add(inspectie.StartTijd.GetValueOrDefault());
            DateTime enddate;
            if (inspectie.EindTijd.GetValueOrDefault() < inspectie.StartTijd.GetValueOrDefault())
            {
                enddate = inspectie.DatumInspectie
                    .GetValueOrDefault()
                    .AddDays(1)
                    .Add(inspectie.EindTijd.GetValueOrDefault());
            }
            else 
            {
                enddate = inspectie.DatumInspectie
                    .GetValueOrDefault()
                    .Add(inspectie.EindTijd.GetValueOrDefault());
            }

            return new PlanningAPIEvent
            {
                id = inspectie.InspectieformulierID,
                text = inspectie.InspectieFormulierTitel,
                start_date = startdate.ToString("yyyy-MM-dd HH:mm"),
                end_date = enddate.ToString("yyyy-MM-dd HH:mm")
            };
        }
    }
}