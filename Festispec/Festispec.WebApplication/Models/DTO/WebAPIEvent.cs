using Festispec.Model;
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

        public static explicit operator WebAPIEvent(Opdracht opdracht)
        {
            if (opdracht!= null)
            {
                return new WebAPIEvent
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