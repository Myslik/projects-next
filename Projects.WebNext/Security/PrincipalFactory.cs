using NArchitecture;
using Projects.Account;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Projects.WebNext.Security
{
    public class PrincipalFactory
    {
        private const string authenticationType = "ApplicationCookie";

        private readonly IServiceBus bus;

        public PrincipalFactory(IServiceBus bus)
        {
            this.bus = bus;
        }

        public async Task<ClaimsPrincipal> CreateAsync(string userName)
        {
            var user = await bus.Request(ClaimsPrincipal.Current, new GetUserCommand(userName));
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            claims.Add(new Claim(ClaimTypes.Email, user.Email));

            var identity = new ClaimsIdentity(claims, authenticationType);
            var principal = new ClaimsPrincipal(identity);
            return principal;
        }
    }
}
