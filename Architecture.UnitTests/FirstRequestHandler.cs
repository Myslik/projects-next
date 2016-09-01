using Architecture.Core;
using System.Threading;
using System.Threading.Tasks;

namespace Architecture.UnitTests
{
    internal class FirstRequestHandler : MessageHandler, IHandleRequest<FirstRequest>
    {
        public async Task Handle(FirstRequest message, CancellationToken cancellationToken)
        {
            await Bus.Send(new SecondRequest(), cancellationToken);
        }
    }
}
