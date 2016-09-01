using Architecture.Core;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Architecture.UnitTests
{
    internal class FailingSimpleEventHandler : IHandleEvent<SimpleEvent>
    {
        public Task Handle(SimpleEvent message, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
