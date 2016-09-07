using Architecture.Core;

namespace Projects.Account
{
    public class GetUserCommand : IRequest<User>
    {
        public string UserName { get; }

        public GetUserCommand(string userName)
        {
            UserName = userName;
        }
    }
}
