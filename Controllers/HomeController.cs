using Microsoft.AspNetCore.Mvc;
using ShopMVC.Database.Model;
using ShopMVC.Repositories.Interface;
using ShopMVC.ViewModel;
using Microsoft.EntityFrameworkCore;
using ShopMVC.Services.Interface;

namespace ShopMVC.Controllers
{
    public class HomeController : Controller
    {
        public readonly IProductServices productServices;
        public HomeController(IProductServices productServices)
        {
            this.productServices = productServices;
        }

        public async Task<IActionResult> Index()
        {
            var model = await productServices.GetHomeViewModelAsync();
            return View(model);
        }
    }
}
