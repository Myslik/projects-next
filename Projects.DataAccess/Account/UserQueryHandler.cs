using Architecture.Core;
using Microsoft.EntityFrameworkCore;
using Projects.Account;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Projects.DataAccess.Account
{
    public class UserQueryHandler : IHandleRequest<UserQuery, IEnumerable<User>>
    {
        private readonly ProjectsContext context;

        public UserQueryHandler(ProjectsContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<User>> Handle(UserQuery request, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await context.Users.Where(request.Expression).AsNoTracking().ToArrayAsync();
        }
    }
}
