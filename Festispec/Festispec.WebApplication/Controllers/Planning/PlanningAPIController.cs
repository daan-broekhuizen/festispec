using Festispec.WebApplication.Models;
using Festispec.WebApplication.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Data.Entity;

namespace Festispec.WebApplication.Controllers.Planning
{
    public class PlanningAPIController : ApiController
    {
        private FestiSpecContext context = new FestiSpecContext();

        public IEnumerable<PlanningAPIEvent> Get()
        {
            HttpContext http = HttpContext.Current;
            int? userID = (int?)http.Session["user"];
            if (userID.HasValue)
            {
                using(context)
                {
                    Account account = context.Account.Find(userID);
                    return context.Inspectieformulier
                        .Include(c => c.Opdracht)
                        .Include(c => c.Ingepland)
                        .ToList()
                        .Where(c => c.Ingepland.Any(i => i.AccountID == userID))
                        .Select(c => (PlanningAPIEvent)c);
                }
            }
            else
                return null;
           
        }

        public PlanningAPIEvent Get(int id)
        {
            HttpContext http = HttpContext.Current;
            int? userID = (int?)http.Session["user"];
            if (userID.HasValue)
                return (PlanningAPIEvent)context.Inspectieformulier
                    .Where(e => e.OpdrachtID == id)
                    .Select(e => (PlanningAPIEvent)e);
            else
                return null;
        }
    }
}
