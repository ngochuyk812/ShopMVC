using Microsoft.AspNetCore.Mvc;

namespace ShopMVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
