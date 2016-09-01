using DryIoc;
using System;

namespace Projects.Web.ServiceProvider
{
    internal sealed class ApplicationServiceProvider : IServiceProvider
    {
        private readonly Container container;

        internal ApplicationServiceProvider(Container container)
        {
            this.container = container;
        }

        public object GetService(Type serviceType)
        {
            return container.Resolve(serviceType);
        }
    }
}