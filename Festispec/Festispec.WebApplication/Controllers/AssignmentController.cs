using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Festispec.WebApplication.Models;

namespace Festispec.WebApplication.Controllers
{
    public class AssignmentController : Controller
    {
        private FestiSpecContext _context;

        public AssignmentController()
        {
            _context = new FestiSpecContext();
        }

        // GET: Assignment
        public ActionResult Search(List<Inspectieformulier> forms)
        {
            IQueryable<Inspectieformulier> inspectieformulier = _context.Inspectieformulier.Include(i => i.Opdracht);
            return View(inspectieformulier.ToList());
        }

        // GET: Assignment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Inspectieformulier inspectieformulier = _context.Inspectieformulier.Find(id);
            if (inspectieformulier == null)
                return HttpNotFound();

            return View(inspectieformulier);
        }
    }
}
