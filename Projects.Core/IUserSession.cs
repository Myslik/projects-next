using System.Security.Principal;

namespace Projects.Core
{
    public interface IUserSession
    {
        IUser CurrentUser { get; }
        void Login(IPrincipal principal);
        void Logout();
    }
}
