using ShopMVC.Database.Model;
using ShopMVC.DTO;
using ShopMVC.Extentions;
using ShopMVC.ViewModel;

namespace ShopMVC.Services.Interface
{
        public interface ICartServices
        {
                Task<CartDTO> CreateCart(int idUser, int idProduct, int ImportId);
                Task<IEnumerable<CartDTO>> GetCartByUser(int id);
                Task<CartDTO> GetCartById(int id);
                Task<CartDTO> ChangeColor(ChangeColorCart model);
        }

}
