using Festispec.WebApplication.Models;
using Festispec.WebApplication.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Festispec.WebApplication.Controllers
{
    public class SchedulerController : ApiController
    {
        private FestiSpecContext db = new FestiSpecContext();
        public IEnumerable<WebAPIEvent> Get()
        {
            return db.Beschikbaarheid_inspecteurs
                    .ToList()
                    .Select(e => (WebAPIEvent)e);
        }

        public WebAPIEvent Get(int id, DateTime date)
        {
            return (WebAPIEvent)db.Beschikbaarheid_inspecteurs.Find(id, date);
        }

        [HttpPost]
        public IHttpActionResult CreateAvailability(WebAPIEvent availability)
        {
            Beschikbaarheid_inspecteurs newAvailability = (Beschikbaarheid_inspecteurs)availability;
            db.Beschikbaarheid_inspecteurs.Add(newAvailability);
            db.SaveChanges();

            return Ok(new
            {
                tid = newAvailability.MedewerkerID,
                action = "inserted"
            });
        }

        [HttpDelete]
        public IHttpActionResult DeleteAvailability(int id, DateTime date)
        {
            Beschikbaarheid_inspecteurs availability = db.Beschikbaarheid_inspecteurs.Find(id, date);
            if (availability != null)
            {
                db.Beschikbaarheid_inspecteurs.Remove(availability);
                db.SaveChanges();
            }

            return Ok(new
            {
                action = "deleted"
            });
        }
    }
}