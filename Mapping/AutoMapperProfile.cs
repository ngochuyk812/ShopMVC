using AutoMapper;
using ShopMVC.Database.Model;
using ShopMVC.DTO;
using ShopMVC.ViewModel;

namespace ShopMVC.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<SignUpViewModel, User>().ReverseMap();
            CreateMap<ReviewViewModel, Review>().ReverseMap();
            CreateMap<Review, ReviewDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<MediaReview, MediaReviewDTO>().ReverseMap();
        }
    }
}
