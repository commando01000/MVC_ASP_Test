using Microsoft.AspNetCore.Mvc;

namespace MVC_Empty_Test.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ContentResult result = new ContentResult();
            result.Content = "Hello From Home !";
            return result;
        }

        public IActionResult About()
        {
            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }
    }
}
