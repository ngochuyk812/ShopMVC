using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopMVC.Services.Interface;
using ShopMVC.ViewModel;
using System.Security.Claims;

namespace ShopMVC.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        public IProductServices productServices { get; set; }
        public ICartServices cartServices { get; set; }    
        public CartController( IProductServices productServices, ICartServices cartServices)
        {
            this.productServices = productServices;
            this.cartServices = cartServices;
        }
        [Authorize]
        public async Task<ActionResult> Get()
        {
            var currentUser = HttpContext.User;
            var idUser = currentUser.FindFirstValue("Id") ?? "15";
            var rs = await cartServices.GetCartByUser(int.Parse(idUser));
            return Ok(rs);
        }

        [Authorize]
        [HttpPost("")]
        public async Task<ActionResult> AddCart(CartViewModel model)
        {
            var import = await productServices.GetImportProduct(model.ImportId);
            if (import == null)
                return BadRequest(new
                {
                    mess = "Sản phẩm không tồn tại"
                });
            
            if (import.Quantity < model.Quantity)
                return BadRequest(new
                {
                    mess = "Số lượng sản phẩm trong kho không đủ"
                });
            var currentUser = HttpContext.User;
            var idUser = currentUser.FindFirstValue("Id") ?? "15";
            var rs = await cartServices.CreateCart(int.Parse(idUser), import.ProductId, model);
            if(rs == null)
                return BadRequest(new
                {
                    mess = "Đã xảy ra lỗi khi thêm vào giỏ hàng"
                });
            return Ok(rs);
        }

    }
}
