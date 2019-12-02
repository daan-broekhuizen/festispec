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
    public class CredentialsController : Controller
    {
        private FestiSpecContext db = new FestiSpecContext();

        // GET: Credentials
        public ActionResult Index()
        {
            IQueryable<Account> account = db.Account.Include(a => a.Rol_lookup);
            return View(account.ToList());
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Account account)
        {
                Account user = db.Account.Where(a => a.Gebruikersnaam.Equals(account.Gebruikersnaam) && a.Wachtwoord.Equals(account.Wachtwoord)).FirstOrDefault();
            if (user != null)
                return RedirectToAction("Index");
            else
                ModelState.AddModelError("Error", "Uw gegevens zijn niet correct.");
            return View(account);
        }




        // GET: Credentials/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Account.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // GET: Credentials/Create
        public ActionResult Create()
        {
            ViewBag.Rol = new SelectList(db.Rol_lookup, "Afkorting", "Betekenis");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AccountID,Gebruikersnaam,Wachtwoord,Rol,Voornaam,Tussenvoegsel,Achternaam,Stad,Straatnaam,Huisnummer,Email,Telefoonnummer,Datum_certificering,Einddatum_certificering,IBAN,Laatste_wijziging")] Account account)
        {
            if (ModelState.IsValid)
            {
                db.Account.Add(account);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Rol = new SelectList(db.Rol_lookup, "Afkorting", "Betekenis", account.Rol);
            return View(account);
        }

        // GET: Credentials/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Account.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            ViewBag.Rol = new SelectList(db.Rol_lookup, "Afkorting", "Betekenis", account.Rol);
            return View(account);
        }

        // POST: Credentials/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AccountID,Gebruikersnaam,Wachtwoord,Rol,Voornaam,Tussenvoegsel,Achternaam,Stad,Straatnaam,Huisnummer,Email,Telefoonnummer,Datum_certificering,Einddatum_certificering,IBAN,Laatste_wijziging")] Account account)
        {
            if (ModelState.IsValid)
            {
                db.Entry(account).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Rol = new SelectList(db.Rol_lookup, "Afkorting", "Betekenis", account.Rol);
            return View(account);
        }

        // GET: Credentials/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Account.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: Credentials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Account account = db.Account.Find(id);
            db.Account.Remove(account);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
