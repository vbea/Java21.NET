﻿using Java21.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace Java21.NET
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            if (ex != null)
            {
                if (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }
                Log.e("Website Error: " + ex.Message);
                Log.e(ex.ToString());
            }
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Redirect("~/404.htm");
            HttpContext.Current.Response.Flush();
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            Application.Lock();
            Application["OnLineUserCount"] = Convert.ToInt32(Application["OnLineUserCount"]) + 1;
            Application.UnLock();
        }

        protected void Session_End(object sender, EventArgs e)
        {
            Application.Lock();
            Application["OnLineUserCount"] = Convert.ToInt32(Application["OnLineUserCount"]) - 1;
            Application.UnLock();
        }
    }
}