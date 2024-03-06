using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ShopMVC.Database.Model;
using ShopMVC.DTO;
using ShopMVC.Extentions;
using ShopMVC.Helper;
using ShopMVC.Repositories.Interface;
using ShopMVC.Services.Interface;
using ShopMVC.Settings;
using ShopMVC.ViewModel;

namespace ShopMVC.Services
{
    public class ReviewServices : IReviewServices
    {
        public UnitOfWork unitOfWork { get; set; }
        public IMapper mapper;

        public ReviewServices(UnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<ReviewDTO> CreateReview(int idUser, ReviewViewModel model)
        {
            var item = mapper.Map<Review>(model);
            item.IdUser = idUser;
            var user = unitOfWork.User.Find(f=>f.Id == idUser);
            List<Media> paths = await FileHelper.SaveRange(model.Files, "Review");
            
            var review = await unitOfWork.Review.AddAsync(item);
            await unitOfWork.CommitAsync();
            List<MediaReview> medias = new List<MediaReview>();
            foreach (var path in paths)
            {
                medias.Add(
                    new MediaReview { 
                        Src = path.Src, 
                        ReviewId = review.Id, 
                        Type = path.Type == Media.TypeEnum.VIDEO ? MediaReview.TypeEnum.VIDEO : MediaReview.TypeEnum.IMAGE });
            }
            unitOfWork.MediaReview.AddRange(medias);
            unitOfWork.Commit();
            review.Medias = medias;
            return mapper.Map<ReviewDTO>(review);
        }

        public async Task<List<ReviewDTO>> GetReviewByProduct(int productId)
        {
            var rs = await unitOfWork.Review.FindListAsync(f=>f.IdProduct == productId,
                i=>i.Include(f=>f.User).Include(f=>f.Medias),
                o=>o.OrderByDescending(f=>f.CreatedDate));
            return mapper.Map<List<ReviewDTO>>(rs);
        }
    }
}
