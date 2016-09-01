using Architecture.Core;
using System.Threading;
using System.Threading.Tasks;

namespace Architecture.UnitTests
{
    internal class SimpleRequestHandler : IHandleRequest<SimpleRequest>
    {
        public Task Handle(SimpleRequest request, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }
    }
}
