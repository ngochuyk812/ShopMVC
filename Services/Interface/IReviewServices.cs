using ShopMVC.Database.Model;
using ShopMVC.DTO;
using ShopMVC.Extentions;
using ShopMVC.ViewModel;

namespace ShopMVC.Services.Interface
{
        public interface IReviewServices
        {
        Task<ReviewDTO> CreateReview(int idUser, ReviewViewModel model);
        Task<List<ReviewDTO>> GetReviewByProduct(int productId);

        }
}
