using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectPortal.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AngularApp2()
        {
            return View();
        }
    }

    public class AzureController : Controller
    {
        public ActionResult WebRole()
        {
            return View();
        }
    }

    public class FrontEndController : Controller
    {
        public ActionResult AngularJs()
        {
            return View();
        }
    }
}