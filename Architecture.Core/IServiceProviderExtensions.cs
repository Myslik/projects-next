using System;
using System.Collections.Generic;

namespace Architecture.Core
{
    public static class IServiceProviderExtensions
    {
        public static IEnumerable<object> GetServices(this IServiceProvider serviceProvider, Type serviceType)
        {
            Guard.AgainstNull(nameof(serviceProvider), serviceProvider);
            Guard.AgainstNull(nameof(serviceType), serviceType);

            var enumerableType = typeof(IEnumerable<>).MakeGenericType(serviceType);
            return (IEnumerable<object>)serviceProvider.GetService(enumerableType);
        }
    }
}
