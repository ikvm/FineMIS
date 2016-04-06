using System;
using System.Configuration;
using System.Web.Routing;
using Microsoft.AspNet.FriendlyUrls;
using StackExchange.Profiling;
using StackExchange.Profiling.Storage;

namespace FineMIS
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            RouteTable.Routes.EnableFriendlyUrls(new FriendlyUrlSettings { AutoRedirectMode = RedirectMode.Permanent });
            MiniProfiler.Settings.Storage = new SqlServerStorage(ConfigurationManager.ConnectionStrings["miniProfiler"].ConnectionString);
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            MiniProfiler.Start();
        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
            MiniProfiler.Stop();
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            Security.AuthenticateRequest();
        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}