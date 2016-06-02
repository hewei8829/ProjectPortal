using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Claims;
using System.Net.Http;
using System.Net;
using System.Web.Http;

namespace ProjectPortal.Controllers
{
    [System.Web.Http.Authorize]
    public class InterviewCodeController : Controller
    {
        // GET: Home

        public ActionResult LeetCode()
        {
            //
            // The Scope claim tells you what permissions the client application has in the service.
            // In this case we look for a scope value of user_impersonation, or full access to the service as the user.
            //
            //if (ClaimsPrincipal.Current.FindFirst("http://schemas.microsoft.com/identity/claims/scope").Value != "user_impersonation")
            //{
            //    throw new HttpResponseException(new HttpResponseMessage { StatusCode = HttpStatusCode.Unauthorized, ReasonPhrase = "The Scope claim does not contain 'user_impersonation' or scope claim not found" });
            //}

            //// A user's To Do list is keyed off of the NameIdentifier claim, which contains an immutable, unique identifier for the user.
            //Claim subject = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier);



            return View();
        }
    }
}