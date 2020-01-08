using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Festispec.WebApplication.Models.DTO
{
    public class BeschikbaarheidAPIEvent
    {
        public int id { get; set; }
        public string text { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }


        public static explicit operator BeschikbaarheidAPIEvent(BeschikbaarheidInspecteurs beschikbaarheid)
        {
            if (beschikbaarheid != null)
            {
                return new BeschikbaarheidAPIEvent
                {
                    text = "Beschikbaarheid",
                    start_date = beschikbaarheid.Datum.ToString("yyyy-MM-dd HH:mm"),
                    end_date = beschikbaarheid.Datum.AddDays(1).ToString("yyyy-MM-dd HH:mm")               
                };
            }
            else
                return null;
        }

        public static explicit operator BeschikbaarheidInspecteurs(BeschikbaarheidAPIEvent apiEvent)
        {
            if (apiEvent != null)
            {
                return new BeschikbaarheidInspecteurs
                {
                    MedewerkerID = apiEvent.id,
                    Datum = DateTime.Parse(
                        apiEvent.start_date,
                        System.Globalization.CultureInfo.InvariantCulture)
                };
            }
            else
                return null;
        }
    }
}