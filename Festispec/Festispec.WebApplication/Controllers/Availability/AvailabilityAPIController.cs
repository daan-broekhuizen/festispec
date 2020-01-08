using Festispec.WebApplication.Models;
using Festispec.WebApplication.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Festispec.WebApplication.Controllers.Availability
{
    public class AvailabilityAPIController : ApiController
    {
        private FestiSpecContext context = new FestiSpecContext();

        public IEnumerable<BeschikbaarheidAPIEvent> Get()
        {
            var session = System.Web.HttpContext.Current.Session;
            int? userID = (int?)session["user"];
            if (userID.HasValue)
            {
                return context.BeschikbaarheidInspecteurs
                    .ToList()
                    .Where(e => e.MedewerkerID == userID.GetValueOrDefault())
                    .Select(e => (BeschikbaarheidAPIEvent)e);
            }
            else
                return null;
        }

        [HttpPost]
        public IHttpActionResult CreateAvailability(BeschikbaarheidAPIEvent apiEvent)
        {
            BeschikbaarheidInspecteurs beschikbaarheid = (BeschikbaarheidInspecteurs)apiEvent;
            var session = System.Web.HttpContext.Current.Session;
            int? userID = (int?)session["user"];
            if(userID.HasValue)
            {
                beschikbaarheid.MedewerkerID = userID.GetValueOrDefault();
                context.BeschikbaarheidInspecteurs.Add(beschikbaarheid);
                context.SaveChanges();
            }


            return Ok(new
            {
                tid = beschikbaarheid.Datum,
                action = "Date added"
            });
        }

        [HttpDelete]
        public IHttpActionResult DeleteAvailability(int id)
        {
            BeschikbaarheidInspecteurs beschikbaarheid = context.BeschikbaarheidInspecteurs.Find(id);
            if (beschikbaarheid != null)
            {
                context.BeschikbaarheidInspecteurs.Remove(beschikbaarheid);
                context.SaveChanges();
            }

            return Ok(new
            {
                action = "deleted"
            });
        }
    }
}
