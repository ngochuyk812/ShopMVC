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
        public async Task<CartDTO> CreateCart(int idUser, int idProduct, int ImportId)
        {
            Cart cart = new Cart
            {
                ImportId = ImportId,
                Quantity = 1
            };
            cart.UserId = idUser;
            cart.ProductId = idProduct;
            var check = await unitOfWork.Cart.FindAsync(f => f.ImportId == ImportId);
            if (check != null)
            {
                check.Quantity++;
                unitOfWork.Cart.Update(check);
                unitOfWork.Commit();
                cart.Id = check.Id;
            }
            else
            {
                var entity = await unitOfWork.Cart.AddAsync(cart);
                await unitOfWork.CommitAsync();
                cart.Id = entity.Id;
            }
            var rs = await unitOfWork.Cart.FindAsync(f => f.Id == cart.Id,
                i => i.Include(f => f.Product).ThenInclude(f => f.Imports)
                .Include(f => f.Product).ThenInclude(f => f.Images));
            return mapper.Map<CartDTO>(rs);
        }

        public async Task<CartDTO> GetCartById(int id)
        {
            var rs = await unitOfWork.Cart.FindAsync(f => f.Id == id);
            return mapper.Map<CartDTO>(rs);
        }

        public async Task<CartDTO> ChangeColor(ChangeColorCart model)
        {
            var rs = await unitOfWork.Cart.FindAsync(f => f.Id == model.CartId);
            if (rs == null)
                return null;
            rs.ImportId = model.ImportId;
            unitOfWork.Cart.Update(rs);
            unitOfWork.Commit();
            return mapper.Map<CartDTO>(rs);
        }

        public int Delete(int id)
        {
            var rs = unitOfWork.Cart.Find(f => f.Id == id);
            if (rs == null) return -1;
            unitOfWork.Cart.Delete(rs);
            unitOfWork.Commit();
            return rs.Id;

        }
    }
}
