using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ShopMVC.Database.Model;
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
    public class AccountServices : IAccountServices
    {
        public IMapper mapper;
        public MailHelper mailHelper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UnitOfWork unitOfWork { get; set; }

        public AccountServices(UnitOfWork unitOfWork, IMapper mapper, MailHelper mailHelper, IHttpContextAccessor httpContextAccessor)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.mailHelper = mailHelper;
            _httpContextAccessor = httpContextAccessor;

        }

        public async Task<User> SignInAsync(LoginViewModel user)
        {
            var u = await unitOfWork.User.FindAsync(f => f.Email == user.Email);
            if (u == null) return null;
            var checkPass = BCrypt.Net.BCrypt.Verify(user.Password, u.Password);
            if (checkPass)
            {
                return u;
            }
            return null;
        }
        public async Task<User> FindByEmail(string email)
        {
            var u = await unitOfWork.User.FindAsync(f => f.Email == email);
            return u;
        }
        public User SignUp(SignUpViewModel user)
        {
            try
            {
                User u = mapper.Map<User>(user);
                u.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                unitOfWork.User.Create(u);
                StringBuilder sb = new StringBuilder();
                string token = JWT.GenerateEmailConfirmationToken(u.Email);
                var request = _httpContextAccessor.HttpContext.Request;
                string currentUrl = request.Scheme + "://" + request.Host + "/account/verification?token=" + token;

                sb.Append("<h1>Vui lòng truy cập đường dẫn để xác thực</h1>");
                sb.AppendLine("<a href = '" + currentUrl + "'>Xác thực</a>");
                mailHelper.Send(u.Email, "Xác thực đăng ký", sb.ToString());
                unitOfWork.Commit();
                return u;
            }
            catch
            {
                return null;
            }

        }

        public async Task<string> Verification(string token)
        {
            ClaimsPrincipal claimsPrincipal = JWT.ValidateToken(token);
            var emailClaim = claimsPrincipal.FindFirst("Email");
            if (emailClaim == null) return null;

            string email = emailClaim.Value;
            var user = await unitOfWork.User.FindAsync(f => f.Email == email);
            if (user == null) return null;
            user.IsVery = 1;
            unitOfWork.User.Update(user);
            unitOfWork.Commit();
            return email;
        }
    }
}
