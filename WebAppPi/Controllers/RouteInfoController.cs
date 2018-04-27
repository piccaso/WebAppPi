using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace WebAppPi.Controllers
{
    [Produces("application/json")]
    [Route("api/route-info")]
    public class RouteInfoController : Controller
    {
        private readonly IUrlHelper _urlHelper;

        public RouteInfoController(IUrlHelper urlHelper)
        {
            _urlHelper = urlHelper;
        }

        [HttpGet("get")]
        public string Get(string controllerName, string actionName)
        {
            return _urlHelper.Action(actionName, controllerName, null, _urlHelper.ActionContext.HttpContext.Request.Scheme);
        }

        [HttpGet("get-random-number/{from}/{to}")]
        public int RandomNumber(int from, int to)
        {
            var r = new Random();
            return r.Next(from, to);
        }
    }
}