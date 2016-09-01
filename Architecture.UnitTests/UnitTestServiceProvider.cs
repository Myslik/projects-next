using Architecture.Core;
using DryIoc;
using System;
using System.Collections.Generic;

namespace Architecture.UnitTests
{
    internal class UnitTestServiceProvider : IServiceProvider, IDisposable
    {
        private readonly Container _container;
        public HandlerRegistrator _registrator;

        public UnitTestServiceProvider()
        {
            _container = new Container();
            _registrator = new HandlerRegistrator((service, impl) =>
            {
                _container.Register(service, impl);
            });
        }

        public void RegisterHandler<THandler>()
            where THandler : IHandle
        {
            _registrator.RegisterHandler<THandler>();
        }

        public object GetService(Type type)
        {
            return _container.Resolve(type);
        }

        public void Dispose()
        {
            this._container.Dispose();
        }
    }

}
