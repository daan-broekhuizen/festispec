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

        public static explicit operator PlanningAPIEvent(Opdracht opdracht)
        {
            if (opdracht!= null)
            {
                return new PlanningAPIEvent
                {
                    id = opdracht.OpdrachtID,
                    text = opdracht.OpdrachtNaam,
                    start_date = opdracht.StartDatum.ToString("yyyy-MM-dd HH:mm"),
                    end_date = opdracht.EindDatum.ToString("yyyy-MM-dd HH:mm")
                };
            }
            else
                return null;
        }

    }
}