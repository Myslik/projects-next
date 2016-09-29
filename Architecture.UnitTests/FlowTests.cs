using Architecture.Core;
using Architecture.Flow;
using Xunit;

namespace Architecture.UnitTests
{
    public class FlowTests
    {
        [Fact]
        public void SimpleTest()
        {
            var builder = new FlowBuilder<SimpleState>();
            var flow = builder
                .Request(s => new SimpleRequest())
                .Request(s => new SecondRequest())
                .Complete();
        }

        public class SimpleState
        {

        }

        public class SimpleRequest : IRequest
        {

        }

        public class SecondRequest : IRequest
        {

        }
    }
}
