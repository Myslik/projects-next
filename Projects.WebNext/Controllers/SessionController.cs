using Architecture.Core;
using Microsoft.AspNetCore.Mvc;
using Projects.Account;
using Projects.WebNext.Security;
using Projects.WebNext.ViewModels;
using System.Threading.Tasks;

namespace Projects.WebNext.Controllers
{
    public class SessionController : Controller
    {
        private readonly IBus bus;
        private readonly PrincipalFactory principalFactory;

        public SessionController(IBus bus, PrincipalFactory principalFactory)
        {
            this.bus = bus;
            this.principalFactory = principalFactory;
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
                var principal = await principalFactory.CreateAsync(model.UserName);
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
    }
}
