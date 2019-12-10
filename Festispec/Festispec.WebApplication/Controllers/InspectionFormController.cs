using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Festispec.WebApplication.Controllers
{
    public class InspectionFormController : Controller
    {
        // GET: InspectionForm
        public ActionResult Index()
        {
            return View();
        }

        // GET: InspectionForm/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: InspectionForm/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: InspectionForm/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: InspectionForm/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: InspectionForm/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: InspectionForm/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: InspectionForm/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
