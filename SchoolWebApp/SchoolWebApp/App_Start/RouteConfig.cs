using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SchoolWebApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                 namespaces: new string[] { "AreasExample.Controllers" }

            );

           // routes.MapMvcAttributeRoutes();

            //AreaRegistration.RegisterAllAreas();

            // routes.MapRoute(
            //"Admin_Default",
            // "Admin/{controller}/{action}/{param}",
            // new { param = UrlParameter.Optional }
            //);
            /*routes.MapRoute(
               name: "AdminArea",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
               namespaces: new string[] { "SchoolWebApp.Areas.AdminArea.Controllers" }
           );
            routes.MapRoute(
               name: "Login",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "Login", action = "Index", id = UrlParameter.Optional },
               namespaces: new string[] { "SchoolWebApp.Controllers" }
           );*/
        }
    }
}
