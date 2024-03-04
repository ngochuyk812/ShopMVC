using Microsoft.AspNetCore.Mvc;

namespace ShopMVC.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Detail(int id)
        {
            return View();
        }

    }
}
