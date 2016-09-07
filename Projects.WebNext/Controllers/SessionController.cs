using Architecture.Core;
using Microsoft.AspNetCore.Mvc;
using Projects.Account;
using Projects.WebNext.ViewModels;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Projects.WebNext.Controllers
{
    public class SessionController : Controller
    {
        private readonly IBus bus;

        public SessionController(IBus bus)
        {
            this.bus = bus;
        }

        [Route("login"), HttpGet]
        public async Task<IActionResult> Login()
        {
            var users = await bus.Send(new UserQuery());
            var model = new LoginViewModel(users);
            return View(model);
        }

        [Route("login"), HttpPost, ValidateAntiForgeryToken] 
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var principal = await CreatePrincipal(model.UserName);
                await HttpContext.Authentication.SignInAsync("ProjectsCookie", principal);
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        [Route("logout"), HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.Authentication.SignOutAsync("ProjectsCookie");
            return RedirectToAction("Login", "Session");
        }

        private async Task<ClaimsPrincipal> CreatePrincipal(string userName)
        {
            var user = await bus.Send(new GetUserCommand(userName));
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            claims.Add(new Claim(ClaimTypes.Email, user.Email));

            var identity = new ClaimsIdentity(claims, "ApplicationCookie");
            var principal = new ClaimsPrincipal(identity);
            return principal;
        }
    }
}
