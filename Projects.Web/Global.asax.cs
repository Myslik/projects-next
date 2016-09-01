using Architecture.Core;
using DryIoc;
using DryIoc.Mvc;
using Projects.Web.ServiceProvider;
using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace Projects.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            var container = new Container();
            var serviceProvider = new ApplicationServiceProvider(container);
            container.RegisterInstance<IServiceProvider>(serviceProvider);
            container.Register<IBus, Bus>();
            container.WithMvc();

            var registrator = new HandlerRegistrator((service, impl) =>
            {
                container.Register(service, impl);
            });
        }
    }
}
