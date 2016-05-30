using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectPortal.Controllers
{
    //This is the azure controller
    public class AzureController : Controller
    {
        public ActionResult WebRole()
        {
            return View();
        }
    }

 
}