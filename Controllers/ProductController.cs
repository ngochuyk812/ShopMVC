using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopMVC.Database.Model;
using ShopMVC.Repositories.Interface;
using ShopMVC.Services.Interface;

namespace ShopMVC.Controllers
{
    public class ProductController : Controller
    {
        public readonly IProductServices productServices;
        public ProductController(IProductServices productServices)
        {
            this.productServices = productServices;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Detail(int id)
        {
            var product = await productServices.GetProductById(id); 
            if(product == null)
            {
                return NotFound();
            }
            return View(product);
        }

    }
}
