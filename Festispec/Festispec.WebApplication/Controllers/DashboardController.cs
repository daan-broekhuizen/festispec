using Festispec.WebApplication.Models;
using Festispec.WebApplication.Models.Repository;
using Festispec.WebApplication.ViewModels.Dashboard;
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
            List<Inspectieformulier> assignmentList;
            if (userId.HasValue)
            {
                using (FestiSpecContext context = new FestiSpecContext())
                {
                    account = context.Account.Find(userId);
                }
                int userIdInt = userId.Value;
                //assignmentList = repo.GetMyAssignments(userIdInt);
            }
            else
                assignmentList = new List<Inspectieformulier>();

            DashboardViewModel dashboardViewModel = new DashboardViewModel();
            //DashboardVM viewData = new DashBoardVM();
            //dashboard.user 
            //dashboard.UpCommingINspections = assignments
            //dashboard 

            

            ViewBag.User = account;
            return View();
        }
    }
}