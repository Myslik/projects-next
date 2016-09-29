using Architecture.Core;
using Architecture.Flow;
using System.Threading.Tasks;
using Xunit;
using System;
using System.Threading;
using System.Collections.Generic;

namespace Architecture.UnitTests
{
    public class FlowTests
    {
        public static List<IRequest> requests = new List<IRequest>();

        [Fact]
        public async Task SimpleTest()
        {
            requests.Clear();
            var provider = new UnitTestServiceProvider();
            provider.RegisterHandler<SimpleRequestHandler>();
            provider.RegisterHandler<SecondRequestHandler>();
            var bus = new Bus(provider);
            var invoker = new FlowInvoker(bus);
            await invoker.Invoke<SimpleFlowDefinition, SimpleState>(new SimpleState());
            Assert.Equal(2, requests.Count);
        }

        public class SimpleFlowDefinition : FlowDefinition<SimpleState>
        {
            protected override FlowBuilder<SimpleState> OnDefinitionCreating(FlowBuilder<SimpleState> builder)
            {
                return builder
                    .Request(s => new SimpleRequest())
                    .Request(s => new SecondRequest());
            }
        }

        public class SimpleState { }

        public class SimpleRequest : IRequest { }

        public class SecondRequest : IRequest { }

        public class SimpleRequestHandler : IHandleRequest<SimpleRequest>
        {
            public Task Handle(SimpleRequest request, CancellationToken cancellationToken = default(CancellationToken))
            {
                requests.Add(request);
                return Task.FromResult(false);
            }
        }

        public class SecondRequestHandler : IHandleRequest<SecondRequest>
        {
            public Task Handle(SecondRequest request, CancellationToken cancellationToken = default(CancellationToken))
            {
                requests.Add(request);
                return Task.FromResult(false);
            }
        }
    }
}
