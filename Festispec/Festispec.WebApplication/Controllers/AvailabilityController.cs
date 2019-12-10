using Festispec.WebApplication.Models;
using Festispec.WebApplication.Models.Repositories;
using System;
using System.Web.Mvc;

namespace Festispec.WebApplication.Controllers
{
    public class AvailabilityController : Controller
    {
        private AvailabilityRepository _repo;
        // GET: Availability
        public ActionResult AvailabilityView()
        {
            return View();
        }

        [HttpPost]
        public JsonResult CreateAvailability(string datestring)
        {
            DateTime date = DateTime.Parse(datestring);
            _repo = new AvailabilityRepository();
            Beschikbaarheid_inspecteurs bi = new Beschikbaarheid_inspecteurs();
            bi.MedewerkerID = 1;
            bi.Datum = date;
            _repo.CreateAvailability(bi);

            return Json(true);
        }

        [HttpDelete]
        public JsonResult DeleteAvailability(string datestring)
        {
            DateTime date = Convert.ToDateTime(datestring);
            _repo = new AvailabilityRepository();
            Beschikbaarheid_inspecteurs bi = new Beschikbaarheid_inspecteurs();
            bi.MedewerkerID = 1;
            bi.Datum = date;
            _repo.DeleteAvailability(bi);

            return Json(true);
        }
    }
}