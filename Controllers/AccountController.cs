using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using ShopMVC.Services;
using ShopMVC.Services.Interface;
using ShopMVC.ViewModel;
using System.Security.Claims;

namespace ShopMVC.Controllers
{
    public class AccountController : Controller
    {
        public IAccountServices AccountServices { get; set; }
        public AccountController(IAccountServices accountServices)
        {
            AccountServices = accountServices;
        }
        public IActionResult Login(string RequestPath)
        {
            var model = new LoginViewModel
            {
                RequestPath = RequestPath
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var check = await AccountServices.SignInAsync(model);
            if (check == null)
            {
                ViewBag.Error = "Email hoặc mật khẩu không chính xác";
                return View(model);
            }

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, check.Name),
                new Claim(ClaimTypes.Email, model.Email)
            };

            ClaimsIdentity identity = new ClaimsIdentity(claims, "cookie");
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                    scheme: "SecurityScheme",
                    principal: principal,
                    properties: new AuthenticationProperties
                    {
                        //IsPersistent = true, // for 'remember me' feature
                        //ExpiresUtc = DateTime.UtcNow.AddMinutes(1)
                    });

            return Redirect(model.RequestPath ?? "/");
        }

        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            var findByMail = await AccountServices.FindByEmail(model.Email);
            if (findByMail != null)
            {
                ViewBag.Error = "Email đã tồn tại trên hệ thống";
                return View(model);
            }

            if (model.Password != model.RePassword)
            {
                ViewBag.Error = "Nhập lại mật khẩu không chính xác";
                return View(model);
            }

            var rs = AccountServices.SignUp(model);
            if(rs == null)
            {
                ViewBag.Error = "Đã xảy ra lỗi trong quá trình đăng ký vui lòng thử lại";
                return View(model);
            }

            ViewBag.Success = "Vui lòng kiểm tra mail để xác thực tại khoản";
            return View(model);
        }
        public async Task<IActionResult> Verification(string? token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return NotFound();
            }
            var rs = await AccountServices.Verification(token);
            if (rs == null) return NotFound();
            TempData["Success"] = "Xác thực thành công bạn có thể truy cập vào tài khoản.";
            return RedirectToAction("Login");
        }
        public async Task<IActionResult> Logout(string? RequestPath)
        {
            await HttpContext.SignOutAsync(
                    scheme: "SecurityScheme");
            return Redirect(RequestPath ?? "~/");
        }

    }
}
