using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Festispec.WebApplication.Models.DTO
{
    public class WebAPIEvent
    {
        public int id { get; set; }
        public string text { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }

        public static explicit operator WebAPIEvent(Inspectieformulier inspectie)
        {
            if (inspectie.Datum_inspectie != null)
            {
                return new WebAPIEvent
                {
                    id = (int)inspectie.OpdrachtID,
                    text = inspectie.InspectieFormulierTitel,
                    start_date = inspectie.Datum_inspectie.GetValueOrDefault().ToString("yyyy-MM-dd HH:mm"),
                    end_date = inspectie.Datum_inspectie.GetValueOrDefault().ToString("yyyy-MM-dd HH:mm")
                };
            }
            else
                return null;
        }

        
    }
}