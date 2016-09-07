using Architecture.Core;
using Microsoft.EntityFrameworkCore;
using Projects.Account;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Projects.DataAccess.Account
{
    public class GetUserCommandHandler : IHandleRequest<GetUserCommand, User>
    {
        private readonly ProjectsContext context;

        public GetUserCommandHandler(ProjectsContext context)
        {
            this.context = context;

        }
        public async Task<User> Handle(GetUserCommand request, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await context.Users.Where(u => u.UserName == request.UserName).SingleAsync();
        }
    }
}
