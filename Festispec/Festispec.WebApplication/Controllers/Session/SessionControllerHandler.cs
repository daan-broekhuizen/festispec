using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.WebHost;
using System.Web.Routing;
using System.Web.SessionState;

namespace Festispec.WebApplication.Controllers.Session
{
    public class SessionControllerHandler : HttpControllerHandler, IRequiresSessionState 
    {
        public SessionControllerHandler(RouteData routeData) : base(routeData)
        {
        }
    }
}