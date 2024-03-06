using AutoMapper;
using ShopMVC.Database.Model;
using ShopMVC.ViewModel;

namespace ShopMVC.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<SignUpViewModel, User>().ReverseMap();
        }
    }
}
