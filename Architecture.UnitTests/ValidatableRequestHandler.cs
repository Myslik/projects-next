using Architecture.Core;
using System.Threading.Tasks;
using System.Threading;

namespace Architecture.UnitTests
{
    internal class ValidatableRequestHandler : IHandleRequest<ValidatableRequest>
    {
        public Task Handle(ValidatableRequest request, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.FromResult(0);
        }
    }
}
