using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Festispec.WebApplication.Models;
using Festispec.WebApplication.Models.Repositories;

namespace Festispec.WebApplication.Controllers
{
    public class AssignmentController : Controller
    {
        private InspectionformRepository _repo;

        public AssignmentController()
        {
            _repo = new InspectionformRepository();
        }

    }
}
