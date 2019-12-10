using Festispec.WebApplication.Models;
using Festispec.WebApplication.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace Festispec.WebApplication.Controllers
{
    public class CalendarController : ApiController
    {
        private AvailabilityRepository _repo;

        [HttpPost]
        public IHttpActionResult CreateAvailability(string datestring)
        {
            DateTime date = Convert.ToDateTime(datestring);
            _repo = new AvailabilityRepository();
            Beschikbaarheid_inspecteurs bi = new Beschikbaarheid_inspecteurs();
            bi.MedewerkerID = 1;
            bi.Datum = date;
            _repo.CreateAvailability(bi);

            return Ok(new
                { 
                tid = bi.MedewerkerID,
                action = "inserted"
            });
        }

        [HttpDelete]
        public IHttpActionResult DeleteAvailability(string datestring)
        {
            DateTime date = Convert.ToDateTime(datestring);
            _repo = new AvailabilityRepository();
            Beschikbaarheid_inspecteurs bi = new Beschikbaarheid_inspecteurs();
            bi.MedewerkerID = 1;
            bi.Datum = date;
            _repo.DeleteAvailability(bi);

            return Ok(new
            {
                action = "deleted"
            });
        }
    }
}
