using Architecture.Core;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Architecture.UnitTests
{
    internal class FailingSimpleRequestHandler : IHandleRequest<SimpleRequest>
    {
        public Task Handle(SimpleRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
