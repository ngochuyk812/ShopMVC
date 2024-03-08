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
        public CartController(IProductServices productServices, ICartServices cartServices)
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
        public async Task<ActionResult> AddCart(int ImportId)
        {
            var import = await productServices.GetImportProduct(ImportId);
            if (import == null)
                return BadRequest(new
                {
                    mess = "Sản phẩm không tồn tại"
                });

            if (import.Quantity < 1)
                return BadRequest(new
                {
                    mess = "Số lượng sản phẩm trong kho không đủ"
                });
            var currentUser = HttpContext.User;
            var idUser = currentUser.FindFirstValue("Id") ?? "15";
            var rs = await cartServices.CreateCart(int.Parse(idUser), import.ProductId, ImportId);
            if (rs == null)
                return BadRequest(new
                {
                    mess = "Đã xảy ra lỗi khi thêm vào giỏ hàng"
                });
            return Ok(rs);
        }
        [Authorize]
        [HttpPut("color")]
        public async Task<ActionResult> ChangeColor([FromBody] ChangeColorCart body)
        {
            var cart = await cartServices.GetCartById(body.CartId);
            if (cart == null)
                return BadRequest(new
                {
                    mess = "Không tồn tại"
                });

            var import = await productServices.GetImportProduct(body.ImportId);
            if (import == null)
                return BadRequest(new
                {
                    mess = "Sản phẩm không tồn tại loại màu này"
                });

            if (import.Quantity < cart.Quantity)
                return BadRequest(new
                {
                    mess = "Số lượng sản phẩm loại màu này không đủ số lượng cho bạn"
                });
            var currentUser = HttpContext.User;
            var idUser = currentUser.FindFirstValue("Id") ?? "15";
            var rs = await cartServices.ChangeColor(body);
            if (rs == null)
                return BadRequest(new
                {
                    mess = "Đã xảy ra lỗi vui lòng thử lại"
                });
            return Ok(new
            {
                CartId = rs.Id
            });
        }

        [Authorize]
        [HttpPut("quantity")]
        public async Task<ActionResult> ChangeQuantity([FromBody] ChangeQuantityCart body)
        {
            var cart = await cartServices.GetCartById(body.CartId);
            if (body.Quantity < 1)
                return BadRequest(new
                {
                    mess = "Số lượng sản phẩm phải lớn hơn 0"
                });
            if (cart == null)
                return BadRequest(new
                {
                    mess = "Không tồn tại"
                });

            var import = await productServices.GetImportProduct(cart.ImportId);
            if (import == null)
                return BadRequest(new
                {
                    mess = "Không tồn tại"
                });

            if (import.Quantity < body.Quantity)
                return BadRequest(new
                {
                    mess = "Số lượng sản phẩm không đủ số lượng cho bạn"
                });
            var rs = await cartServices.ChangeQuantity(body);
            if (rs == null)
                return BadRequest(new
                {
                    mess = "Đã xảy ra lỗi vui lòng thử lại"
                });
            return Ok(body);
        }

        [Authorize]
        [HttpDelete("")]
        public ActionResult Remove(int id)
        {
            var rs = cartServices.Delete(id);
            if (rs == -1)
                return BadRequest(new
                {
                    mess = "Đã xảy ra lỗi vui lòng thử lại"
                });

            return Ok(new
            {
                Id = rs
            });
        }
    }
}
