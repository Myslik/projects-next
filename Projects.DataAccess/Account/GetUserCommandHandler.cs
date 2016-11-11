using Microsoft.EntityFrameworkCore;
using NArchitecture;
using Projects.Account;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Projects.DataAccess.Account
{
    public class GetUserCommandHandler : RequestHandler<GetUserCommand, User>
    {
        private readonly ProjectsContext db;

        public GetUserCommandHandler(ProjectsContext db)
        {
            this.db = db;

        }

        protected override async Task Handle(RequestHandlerContext<User> context, GetUserCommand request)
        {
            context.Response = await this.db.Users.Where(u => u.UserName == request.UserName).SingleAsync();
        }
    }
}
