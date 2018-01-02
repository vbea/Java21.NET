using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Java21.NET
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{resource}.asmx/{*pathInfo}");
            //routes.IgnoreRoute("Views/Java21/index1.html");

            routes.MapRoute(
                name: "Action1Html", // action伪静态  
                url: "{action}.html",// 带有参数的 URL  
                defaults: new { controller = "Java21", action = "Home", id = UrlParameter.Optional }// 参数默认值  
            );
            //routes.MapRoute(
            //   "Action2Html", // action伪静态  
            //   "java21/{action}.html",// 带有参数的 URL  
            //   new { controller = "Java21", action = "Home", id = UrlParameter.Optional }// 参数默认值  
            //);
            routes.MapRoute(
                name: "Material",
                url: "material/{id}.html",
                defaults: new { controller = "Material", action = "Material", id = "0" }
            );
            routes.MapRoute(
                name: "Java",
                url: "{action}",
                defaults: new { controller = "Java21", action = "Home" }
            );
            routes.MapRoute(
                name: "Users",
                url: "Users/{action}",
                defaults: new { controller = "Users", action = "Login" }
            );
            routes.MapRoute(
                name: "Artical",
                url: "article/{id}.html",
                defaults: new { controller = "Article", action = "Views", id = "views" }
            );
            routes.MapRoute(
                name: "Artical2",
                url: "comment/{id}.html",
                defaults: new { controller = "Article", action = "Comment", id = "views" }
            );
            routes.MapRoute(
               name: "Artical3",
               url: "java/{id}.html",
               defaults: new { controller = "Article", action = "java", id = "views" }
           );
            routes.MapRoute(
              name: "Artical5",
              url: "video/{id}.html",
              defaults: new { controller = "Article", action = "video", id = "1" }
          );
            routes.MapRoute(
              name: "Artical6",
              url: "v/{id}.html",
              defaults: new { controller = "Article", action = "video", id = "1" }
          );
            routes.MapRoute(
              name: "Artical4",
              url: "c/{id}.html",
              defaults: new { controller = "Article", action = "c", id = "views" }
          );
            // routes.MapRoute(
            //    name: "Artical1",
            //    url: "article/{action}/{id}",
            //    defaults: new { controller = "article", action = "Views", id = "1000" }
            //);
            routes.MapRoute(
                name: "Java1",
                url: "Java21/{action}",
                defaults: new { controller = "Java21", action = "Home" }
            );
            routes.MapRoute(
                name: "Java2",
                url: "{action}/{id}",
                defaults: new { controller = "Java21", action = "Home", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Java21", action = "Home", id = UrlParameter.Optional }
            );
        }
    }
}