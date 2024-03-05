using ShopMVC.Database.Model;
using ShopMVC.Extentions;
using ShopMVC.ViewModel;

namespace ShopMVC.Services.Interface
{
        public interface IProductServices
        {
                Task<HomeViewModel> GetHomeViewModelAsync();
                Task<Product> GetProductById(int id);
                Task<Pagination<Product>> PageProduct(ProductViewModel viewModel);

        }
}
