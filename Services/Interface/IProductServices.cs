using ShopMVC.Database.Model;
using ShopMVC.ViewModel;

namespace ShopMVC.Services.Interface
{
    public interface IProductServices
    {
        Task<HomeViewModel> GetHomeViewModelAsync();
        Task<Product> GetProductById(int id);  

    }
}
