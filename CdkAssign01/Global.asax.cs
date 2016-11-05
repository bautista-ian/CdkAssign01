using Autofac;
using Autofac.Integration.Mvc;
using CdkAssign01.BAL;
using CdkAssign01.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace CdkAssign01
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            #region Autofac IoC
            var builder = new ContainerBuilder();

            // You can register controllers all at once using assembly scanning...
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            //register components
            builder.RegisterType<CustomersEngine>().As<ICustomersEngine>();
            builder.RegisterType<OrdersEngine>().As<IOrdersEngine>();
            builder.RegisterType<CustomersRepository>().As<ICustomersRepository>();
            builder.RegisterType<OrdersRepository>().As<IOrdersRepository>();

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            #endregion
        }
    }
}
