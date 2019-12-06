using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Festispec.WebApplication.Models.Repositories
{
    public class WebAPIEvent
    {
        public int id { get; set; }
        public string date { get; set; }

        public static explicit operator WebAPIEvent(Beschikbaarheid_inspecteurs beschikbaarheid)
        {
            return new WebAPIEvent
            {
                id = beschikbaarheid.MedewerkerID,
                date = beschikbaarheid.Datum.ToString("yyyy-MM-dd")
            };
        }

        public static explicit operator Beschikbaarheid_inspecteurs(WebAPIEvent beschikbaarheid)
        {
            return new Beschikbaarheid_inspecteurs
            {
                MedewerkerID = beschikbaarheid.id,
                Datum = DateTime.Parse(
                    beschikbaarheid.date, System.Globalization.CultureInfo.InvariantCulture)
            };
        }
    }
}