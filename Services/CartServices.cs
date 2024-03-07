using AutoMapper;
using Microsoft.AspNetCore.Identity;
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
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ShopMVC.Services
{
    public class CartServices : ICartServices
    {
        public IMapper mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UnitOfWork unitOfWork { get; set; }

        public CartServices(UnitOfWork unitOfWork, IMapper mapper, MailHelper mailHelper, IHttpContextAccessor httpContextAccessor)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;

        }
        public async Task<IEnumerable<CartDTO>> GetCartByUser(int id)
        {
            var rs = await unitOfWork.Cart.FindListAsync(f => f.UserId == id,
                i => i.Include(f => f.Product).ThenInclude(f => f.Imports)
                .Include(f => f.Product).ThenInclude(f => f.Images));
            return mapper.Map<IEnumerable<CartDTO>>(rs);
        }
        public async Task<CartDTO> CreateCart(int idUser,int idProduct, CartViewModel model)
        {
            Cart cart = mapper.Map<Cart>(model);
            cart.UserId = idUser;
            cart.ProductId = idProduct;
            var entity = await unitOfWork.Cart.AddAsync(cart);
            await unitOfWork.CommitAsync();
            var rs = await unitOfWork.Cart.FindAsync(f => f.Id == entity.Id,
                i=>i.Include(f=>f.Product).ThenInclude(f=>f.Imports)
                .Include(f => f.Product).ThenInclude(f => f.Images));
            return mapper.Map<CartDTO>(rs);
        }
    }   
}
