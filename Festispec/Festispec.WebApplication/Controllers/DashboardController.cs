using Festispec.WebApplication.Models;
using Festispec.WebApplication.Models.Repository;
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
            UserRepository repo = new UserRepository();
            if(userId.HasValue)
            {
                using(FestiSpecContext context = new FestiSpecContext())
                {
                    account = context.Account.Find(userId);
                }
                int userIdInt = userId.Value;
                //assignmentList = repo.GetMyAssignments(userIdInt);
            }

            
            //DashboardVM viewData = new DashBoardVM();
            //dashboard.user 
            //dashboard.UpCommingINspections = assignments
            //dashboard 

            

            ViewBag.User = account;
            return View(/*viewData*/);
        }
    }
}