using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RaceControl.Controllers
{
    [Authorize(Roles = "AppAdmin")]
    public class SystemAdminController : Controller
    {
        // GET: SystemAdmin

        public ActionResult Index()
        {
            return View();
        }
    }
}