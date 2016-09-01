using Architecture.Core;
using System.Threading;
using System.Threading.Tasks;

namespace Architecture.UnitTests
{
    internal class SimpleEventHandler : IHandleEvent<SimpleEvent>
    {
        public Task Handle(SimpleEvent message, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }
    }
}
