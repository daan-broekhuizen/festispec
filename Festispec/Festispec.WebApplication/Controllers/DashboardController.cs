using Festispec.WebApplication.Models;
using Festispec.WebApplication.Models.Repositories;
using Festispec.WebApplication.ViewModels.Dashboard;
using Festispec.WebApplication.ViewModels.Inspection;
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
            InspectionformRepository repo = new InspectionformRepository();
            List<Inspectieformulier> assignmentList;
            if (userId.HasValue)
            {
                using (FestiSpecContext context = new FestiSpecContext())
                {
                    account = context.Account.Find(userId);
                }
                int userIdInt = userId.Value;
                assignmentList = repo.GetInspectionforms(userIdInt);
            }
            else
                assignmentList = new List<Inspectieformulier>();

            DashboardViewModel dashboardViewModel = new DashboardViewModel
            {
                Account = account,
                Inspectionform = assignmentList.Select(i => new InspectionformViewModel(i)).ToList()
            };
            return View(dashboardViewModel);
        }
    }
}