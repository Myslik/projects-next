using Architecture.Core;
using System.Threading;
using System.Threading.Tasks;

namespace Architecture.UnitTests
{
    internal class SecondRequestHandler : MessageHandler, IHandleRequest<SecondRequest>
    {
        public Task Handle(SecondRequest message, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.FromResult(0);
        }
    }
}
