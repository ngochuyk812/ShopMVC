using Microsoft.EntityFrameworkCore;
using ShopMVC.Database.Model;
using ShopMVC.Repositories.Interface;
using ShopMVC.Services.Interface;
using ShopMVC.ViewModel;

namespace ShopMVC.Services
{
    public class ProductServices: IProductServices
    {
        public UnitOfWork unitOfWork { get; set; }

        public ProductServices(UnitOfWork unitOfWork) { 
            this.unitOfWork = unitOfWork;
        }

        public async Task<HomeViewModel> GetHomeViewModelAsync()
        {
            var clock = await unitOfWork.Product.PagiAsync(1, 4, f => f.Category.Name.Contains("Đồng hồ"), f => f.Include(f => f.Images), o => o.OrderByDescending(f => f.CreatedDate));
            var jewelry = await unitOfWork.Product.PagiAsync(1, 4, f => f.Category.Name.Contains("Phụ kiện"), f => f.Include(f => f.Images), o => o.OrderByDescending(f => f.CreatedDate));
            var model = new HomeViewModel
            {
                NewClock = clock.Data,
                NewJewelry = jewelry.Data,
            };
            return model;
        }

        public async Task<Product> GetProductById(int id)
        {
            var product = await unitOfWork.Product.FindAsync(f => f.Id == id, f => f.Include(f => f.Images));
            return product; 
        }
    }
}
