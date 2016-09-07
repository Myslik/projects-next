namespace Projects.Account
{
    public interface IUserSession
    {
        User CurrentUser { get; }
        void Login(int userId);
        void Logout();
    }
}
