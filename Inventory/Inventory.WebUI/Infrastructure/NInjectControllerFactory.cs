using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;
using Ninject.Modules;
using Inventory.Core.Data;
using Inventory.Core.Entities;

namespace Inventory.WebUI.Infrastructure
{
    public class NInjectControllerFactory : DefaultControllerFactory
    {
        // A Ninject "kernel" is the thing that can supply object instances
        private IKernel kernel = new StandardKernel(new InventoryServices());

        // ASP.NET MVC calls this to get the controller for each request
        protected override IController GetControllerInstance(RequestContext context, Type controllerType)
        {
            if (controllerType == null)
                return null;
            return (IController)kernel.Get(controllerType);
        }

        // Configures how abstract service types are mapped to concrete implementations
        private class InventoryServices : NinjectModule
        {
            public override void Load()
            {
                Bind<IMaterialsRepository>()
                    .To<MaterialsRepository>()
                    .WithConstructorArgument("sessionFactory", MvcApplication.SessionFactory);
            }
        }
    }
}