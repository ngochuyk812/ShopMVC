using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopMVC.Services.Interface;
using ShopMVC.ViewModel;
using System.Security.Claims;

namespace ShopMVC.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        public IReviewServices reviewServices { get; set; }
        public IProductServices productServices { get; set; }

        public ReviewController(IReviewServices reviewServices, IProductServices productServices)
        {
            this.reviewServices = reviewServices;
            this.productServices = productServices;
        }
        [HttpPost("")]
        [Authorize]
        public async Task<IActionResult> CreateReview(ReviewViewModel model)
        {
            var currentUser = HttpContext.User;
            var idUser = currentUser.FindFirstValue("Id");
            if(string.IsNullOrEmpty(model.Content) == null && model.Files?.Count() == 0)
            {
                return BadRequest(new
                {
                    mess = "Vui lòng nhập nội dùng đánh giá"
                });
            }
            var product = await productServices.GetProductById(model.IdProduct);
            if(product == null)
                return BadRequest(new
                {
                    mess = "Sản phẩm không tồn tại"
                });
            
            var item = await reviewServices.CreateReview(int.Parse(idUser), model);
            return Ok(item);
        }
        [HttpGet("")]
        public async Task<IActionResult> Index(int product)
        {
            var rs = await reviewServices.GetReviewByProduct(product);
            return Ok(rs);
        }

    }
}
