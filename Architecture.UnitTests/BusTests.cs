using Architecture.Core;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using Xunit;
using System.Threading.Tasks;

namespace Architecture.UnitTests
{
    public class BusTests
    {
        internal UnitTestServiceProvider ServiceProvider { get; private set; }
        internal IBus Bus { get; private set; }

        public BusTests()
        {
            ServiceProvider = new UnitTestServiceProvider();
            Bus = new Bus(ServiceProvider);
        }

        public void Dispose()
        {
            ServiceProvider.Dispose();
        }

        [Fact]
        public async Task SendSimpleEventTest()
        {
            ServiceProvider.RegisterHandler<SimpleEventHandler>();
            await Bus.Send(new SimpleEvent(), CancellationToken.None);
        }

        [Fact]
        public async Task SendNullSimpleEventTest()
        {
            SimpleEvent @event = null;
            await Assert.ThrowsAsync<ArgumentNullException>(
                () => Bus.Send(@event));
        }

        [Fact]
        public async Task SendSimpleEventWithoutHandlerTest()
        {
            await Bus.Send(new SimpleEvent(), CancellationToken.None);
        }

        [Fact]
        public async Task SendSimpleEventWithFailingHandlerTest()
        {
            ServiceProvider.RegisterHandler<FailingSimpleEventHandler>();
            await Assert.ThrowsAsync<NotImplementedException>(
                () => Bus.Send(new SimpleEvent(), CancellationToken.None));
        }

        [Fact]
        public async Task SendSimpleRequestTest()
        {
            ServiceProvider.RegisterHandler<SimpleRequestHandler>();
            await Bus.Send(new SimpleRequest(), CancellationToken.None);
        }

        [Fact]
        public async Task SendNullSimpleRequestTest()
        {
            SimpleRequest request = null;
            await Assert.ThrowsAsync<ArgumentNullException>(
                () => Bus.Send(request));
        }

        [Fact]
        public async Task SendSimpleRequestWithoutHandlerTest()
        {
            await Assert.ThrowsAsync<InvalidOperationException>(
                () => Bus.Send(new SimpleRequest(), CancellationToken.None));
        }

        [Fact]
        public async Task SendSimpleRequestWithFailingHandlerTest()
        {
            ServiceProvider.RegisterHandler<FailingSimpleRequestHandler>();
            await Assert.ThrowsAsync<NotImplementedException>(
                () => Bus.Send(new SimpleRequest(), CancellationToken.None));
        }

        [Fact]
        public async Task SendComplexRequestTest()
        {
            ServiceProvider.RegisterHandler<ComplexRequestHandler>();
            int result = await Bus.Send(new ComplexRequest(), CancellationToken.None);
            Assert.Equal(ComplexRequestHandler.RETURN_VALUE, result);
        }

        [Fact]
        public async Task SendNullComplexRequestTest()
        {
            ComplexRequest request = null;
            await Assert.ThrowsAsync<ArgumentNullException>(
                () => Bus.Send(request));
        }

        [Fact]
        public async Task SendComplexRequestWithoutHandlerTest()
        {
            await Assert.ThrowsAsync<InvalidOperationException>(
                () => Bus.Send(new ComplexRequest(), CancellationToken.None));
        }

        [Fact]
        public async Task SendComplexRequestWithFailingHandlerTest()
        {
            ServiceProvider.RegisterHandler<FailingComplexRequestHandler>();
            await Assert.ThrowsAsync<NotImplementedException>(
                () => Bus.Send(new ComplexRequest(), CancellationToken.None));
        }

        [Fact]
        public async Task SendRequestChainTest()
        {
            ServiceProvider.RegisterHandler<FirstRequestHandler>();
            ServiceProvider.RegisterHandler<SecondRequestHandler>();
            await Bus.Send(new FirstRequest());
        }

        [Fact]
        public async Task SendValidRequestTest()
        {
            ServiceProvider.RegisterHandler<ValidatableRequestHandler>();
            await Bus.Send(new ValidatableRequest { Name = "Request", IsValid = true });
        }

        [Fact]
        public async Task SendInvalidRequestTest()
        {
            await Assert.ThrowsAsync<ValidationException>(
                () => Bus.Send(new ValidatableRequest { Name = "Request" }, CancellationToken.None));
        }

        [Fact]
        public async Task SendAnotherInvalidRequestTest()
        {
            await Assert.ThrowsAsync<ValidationException>(
                () => Bus.Send(new ValidatableRequest { IsValid = true }, CancellationToken.None));
        }
    }
}
