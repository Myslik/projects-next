using Architecture.Core;
using System;
using Xunit;

namespace Architecture.UnitTests
{
    public class HandlerRegistratorTests
    {
        [Fact]
        public void RegisterEventHandlerTest()
        {
            var registrator = new HandlerRegistrator((service, impl) =>
            {
                Assert.Equal(typeof(IHandleEvent<SimpleEvent>), service);
            });
            registrator.RegisterHandler<SimpleEventHandler>();
        }

        [Fact]
        public void RegisterRequestHandlerTest()
        {
            var registrator = new HandlerRegistrator((service, impl) =>
            {
                Assert.Equal(typeof(IHandleRequest<SimpleRequest>), service);
            });
            registrator.RegisterHandler<SimpleRequestHandler>();
        }

        [Fact]
        public void RegisterRequestHandlerWithResponseTest()
        {
            var registrator = new HandlerRegistrator((service, impl) =>
            {
                Assert.Equal(typeof(IHandleRequest<ComplexRequest, int>), service);
            });
            registrator.RegisterHandler<ComplexRequestHandler>();
        }

        [Fact]
        public void RegisterUnknownHandlerTest()
        {
            var registrator = new HandlerRegistrator((service, impl) =>
            {
                throw new NotImplementedException();
            });
            Assert.Throws<InvalidOperationException>(() =>
            {
                registrator.RegisterHandler<UnknownHandler>();
            });
        }
    }
}
