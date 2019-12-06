using Festispec.WebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Festispec.WebApplication.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            int? userId = (int?) Session["user"];
            Account account = null;
            if(userId.HasValue)
            {
                using(var context = new FestiSpecContext())
                {
                    account = context.Account.Find(userId);
                }
            }

            ViewBag.User = account;
            return View();
        }
    }
}