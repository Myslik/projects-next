using Architecture.Core;
using System.Web.Mvc;

namespace Projects.Web.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(IBus bus)
        {

        }

        [Route, HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}