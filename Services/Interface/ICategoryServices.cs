using ShopMVC.Database.Model;
using ShopMVC.Extentions;
using ShopMVC.ViewModel;

namespace ShopMVC.Services.Interface
{
        public interface ICategoryServices
        {
                Task<IEnumerable<Category>> ListAsync();

        }
}
