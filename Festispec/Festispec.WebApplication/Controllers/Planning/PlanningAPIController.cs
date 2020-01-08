using Festispec.WebApplication.Models;
using Festispec.WebApplication.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Festispec.WebApplication.Controllers.Planning
{
    public class PlanningAPIController : ApiController
    {
        private FestiSpecContext context = new FestiSpecContext();

        public IEnumerable<PlanningAPIEvent> Get()
        {
            var session = System.Web.HttpContext.Current.Session;
            int? userID = (int?)session["user"];
            if (userID.HasValue)
            {
                return context.Opdracht
                    .ToList()
                    .Where(e => e.MedewerkerID == userID.GetValueOrDefault())
                    .Select(e => (PlanningAPIEvent)e);
            }
            else
                return null;
           
        }

        public PlanningAPIEvent Get(int id)
        {
            return (PlanningAPIEvent)context.Opdracht.Find(id);
        }
    }
}
