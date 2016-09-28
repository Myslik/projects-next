using Architecture.Core;
using Projects.Account;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Projects.WebNext.Security
{
    public class PrincipalFactory
    {
        private const string authenticationType = "ApplicationCookie";

        private readonly IBus bus;

        public PrincipalFactory(IBus bus)
        {
            this.bus = bus;
        }

        public async Task<ClaimsPrincipal> CreateAsync(string userName)
        {
            var user = await bus.Send(new GetUserCommand(userName));
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            claims.Add(new Claim(ClaimTypes.Email, user.Email));

            var identity = new ClaimsIdentity(claims, authenticationType);
            var principal = new ClaimsPrincipal(identity);
            return principal;
        }
    }
}
