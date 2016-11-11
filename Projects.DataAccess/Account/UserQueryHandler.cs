using Microsoft.EntityFrameworkCore;
using NArchitecture;
using Projects.Account;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projects.DataAccess.Account
{
    public class UserQueryHandler : RequestHandler<UserQuery, IEnumerable<User>>
    {
        private readonly ProjectsContext db;

        public UserQueryHandler(ProjectsContext db)
        {
            this.db = db;
        }

        protected override async Task Handle(RequestHandlerContext<IEnumerable<User>> context, UserQuery request)
        {
            context.Response = await db.Users.Where(request.Expression).AsNoTracking().ToArrayAsync();
        }
    }
}
