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
        private FestiSpecContext db = new FestiSpecContext();

        public IEnumerable<WebAPIEvent> Get()
        {
            return db.Inspectieformulier
                    .ToList()
                    .Select(e => (WebAPIEvent)e);
        }

        public WebAPIEvent Get(int id)
        {
            return (WebAPIEvent)db.Inspectieformulier.Find(id);
        }
    }
}
