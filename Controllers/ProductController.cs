using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShopMVC.Database.Model;
using ShopMVC.Repositories.Interface;
using ShopMVC.Services.Interface;
using ShopMVC.ViewModel;
using System.Security.Claims;

namespace ShopMVC.Controllers
{
    public class ProductController : Controller
    {
        public readonly IProductServices productServices;
        public readonly ICategoryServices categoryServices;

        public ProductController(IProductServices productServices, ICategoryServices categoryServices)
        {
            this.productServices = productServices;
            this.categoryServices = categoryServices;
        }

  
        public async Task<IActionResult> Index(ProductViewModel viewModel)
        {
            var page = await productServices.PageProduct(viewModel);
            var cat = await GetAllCategory();
            var cate_select = new SelectList(cat, "Id", "Name", viewModel.Category);
            ViewBag.Cat = cate_select;
            viewModel.Data = page;
            return View(viewModel);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var product = await productServices.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            var currentUser = HttpContext.User;
            
            
            string name = currentUser?.Identity?.Name;
            string email = currentUser?.FindFirstValue(ClaimTypes.Email);
            ViewBag.Name = name;
            ViewBag.Email = email;
            ViewBag.Logged = currentUser.Identity.IsAuthenticated;
            return View(product);
        }

        private async Task<List<Category>> GetAllCategory()
        {
            var cat = (await categoryServices.ListAsync()).ToList() ;

            cat.Insert(0, new Category
            {
                Id = -1,
                Name = "Tất cả danh mục"
            });
            return cat;
        }

    }
}
