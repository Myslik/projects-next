using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Projects.WebNext.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        [Route(""), HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [Route("error"), HttpGet]
        public IActionResult Error()
        {
            return View();
        }
    }
}
