using Microsoft.AspNetCore.Mvc;
using ShopMVC.Database.Model;
using ShopMVC.Repositories.Interface;
using ShopMVC.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace ShopMVC.Controllers
{
    public class HomeController : Controller
    {
        public readonly UnitOfWork unitOfWork;
        public HomeController(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var clock = await unitOfWork.Product.PagiAsync(1, 4, f=>f.Category.Name.Contains("Đồng hồ"), f=>f.Include(f=>f.Images), o=>o.OrderByDescending(f=>f.CreatedDate));
            var jewelry = await unitOfWork.Product.PagiAsync(1, 4, f => f.Category.Name.Contains("Phụ kiện"), f => f.Include(f => f.Images), o => o.OrderByDescending(f => f.CreatedDate));
            var model = new HomeViewModel
            {
                NewClock = clock.Data,
                NewJewelry = jewelry.Data,
            };
            return View(model);
        }
    }
}
