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
using System.Web.UI.WebControls;

namespace Festispec.WebApplication.Controllers.Planning
{
    public class PlanningAPIController : ApiController
    {

        public IEnumerable<PlanningAPIEvent> Get()
        {
            HttpContext http = HttpContext.Current;
            int? userID = (int?)http.Session["user"];
            if (userID.HasValue)
            {
                using(FestiSpecContext context = new FestiSpecContext())
                {
                    IEnumerable<PlanningAPIEvent> list = context.Inspectieformulier
                        .Include(c => c.Ingepland)
                        .Where(c => c.Ingepland.Any(i => i.AccountID == userID))
                        .ToList()
                        .Select(c => (PlanningAPIEvent)c);
                    return list;
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
                using (FestiSpecContext context = new FestiSpecContext())
                {
                    return (PlanningAPIEvent)context.Inspectieformulier
                        .Where(e => e.OpdrachtID == id)
                        .Select(e => (PlanningAPIEvent)e);
                }
            else
                return null;
        }
    }
}
