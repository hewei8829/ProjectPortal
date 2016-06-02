using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Optimization;


namespace ProjectPortal
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            GlobalFilters.Filters.Add(new HandleErrorAttribute());

        }

        //public void ConfigureAuth(IAppBuilder app)
        //{
        //    app.UseWindowsAzureActiveDirectoryBearerAuthentication(
        //        new WindowsAzureActiveDirectoryBearerAuthenticationOptions
        //        {
        //            Audience = ConfigurationManager.AppSettings["ida:Audience"],
        //            Tenant = ConfigurationManager.AppSettings["ida:Tenant"]
        //        });
        //}
    }
}
