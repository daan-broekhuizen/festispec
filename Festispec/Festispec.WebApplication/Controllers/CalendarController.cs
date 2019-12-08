using Festispec.WebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace Festispec.WebApplication.Controllers
{
    public class CalendarController : ApiController
    {
        public IEnumerable<Beschikbaarheid_inspecteurs> Get()
        {
            using(FestiSpecContext context = new FestiSpecContext())
            {
                
                        
            }
        }

        public Beschikbaarheid_inspecteurs Get(int id, DateTime date)
        {
            using (FestiSpecContext context = new FestiSpecContext())
            {
                return context.Beschikbaarheid_inspecteurs.Find(id, date);
            }
        }

        [HttpPost]
        public IHttpActionResult CreateAvailability(Beschikbaarheid_inspecteurs availability)
        {
            using (FestiSpecContext context = new FestiSpecContext())
            {
                context.Beschikbaarheid_inspecteurs.Add(availability);
                context.SaveChanges();
            }

            return Ok(new
                { 
                tid = availability.MedewerkerID,
                action = "inserted"
            });
        }

        [HttpDelete]
        public IHttpActionResult DeleteAvailability(int id, DateTime date)
        {
            
        }
    }
}
