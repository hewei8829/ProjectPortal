using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectPortal.Controllers
{
    public class ToolsController : Controller
    {
        // GET: Tools
        public ActionResult VisualStudioCodeView()
        {
            return View();
        }
    }
}