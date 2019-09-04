using System.Web.Mvc;

namespace IdentityWebApp.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

    }
}