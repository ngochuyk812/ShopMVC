using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopMVC.Services;
using ShopMVC.Services.Interface;
using ShopMVC.ViewModel;
using System.Security.Claims;

namespace ShopMVC.Controllers
{
    public class ProfileController : Controller
    {
        public IAccountServices AccountServices { get; set; }
        public ProfileController(IAccountServices accountServices)
        {
            AccountServices = accountServices;
        }
        [Authorize]
        public IActionResult Index(string RequestPath)
        {
            var model = new LoginViewModel
            {
                RequestPath = RequestPath
            };
            return View(model);
        }
        public async Task<IActionResult> Logout(string? RequestPath)
        {
            await HttpContext.SignOutAsync(
                    scheme: "SecurityScheme");
            return Redirect(RequestPath ?? "~/");
        }

    }
}
