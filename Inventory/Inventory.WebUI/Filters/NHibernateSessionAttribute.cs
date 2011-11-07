using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NHibernate;
using NHibernate.Context;

namespace Inventory.WebUI.Filters
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple=false)]
    public class NHibernateSessionAttribute : ActionFilterAttribute
    {

        public NHibernateSessionAttribute()
        {
            Order = 100;
        }

        protected ISessionFactory SessionFactory
        {
            get { return MvcApplication.SessionFactory; }
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ISession session = SessionFactory.OpenSession();
            CurrentSessionContext.Bind(session);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            ISession session = CurrentSessionContext.Unbind(SessionFactory);
            session.Close();
        }
    }
}