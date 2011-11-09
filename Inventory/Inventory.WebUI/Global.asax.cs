using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using NHibernate;
using NHibernate.Cfg;
using Inventory.WebUI.Infrastructure;

namespace Inventory.WebUI
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static ISessionFactory SessionFactory { get; private set; }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{page}", // URL with parameters
                new { controller = "Materials", action = "List", page = UrlParameter.Optional } // Parameter defaults
            );
            routes.MapRoute(null, "{controller}/{action}");
            routes.MapRoute(null, "", new { controller = "Materials", action = "List", page = 1, sort_col = (string) null, sort_desc = true });
        }

        protected void Application_Start()
        {
            //Setup Route Mapping Tables
            AreaRegistration.RegisterAllAreas();
            RegisterRoutes(RouteTable.Routes);

            //Setup Log4Net
            log4net.Config.XmlConfigurator.Configure();

            //Setup NHibernate Profiling 
            HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
            
            //Create session factory
            Configuration nhConfig = new Configuration().Configure();
            SessionFactory = nhConfig.BuildSessionFactory();

            //Setup NInject Controller Factory
            ControllerBuilder.Current.SetControllerFactory(new Inventory.WebUI.Infrastructure.NInjectControllerFactory());
        }

    }
}