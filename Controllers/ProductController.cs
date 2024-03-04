using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopMVC.Database.Model;
using ShopMVC.Repositories.Interface;

namespace ShopMVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly UnitOfWork unitOfWork;

        public ProductController(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Detail(int id)
        {
            var product = await unitOfWork.Product.FindAsync(f => f.Id == id, f=>f.Include(f=>f.Images));
            if(product == null)
            {
                return NotFound();
            }
            return View(product);
        }

    }
}
