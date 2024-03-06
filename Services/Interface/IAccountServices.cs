using ShopMVC.Database.Model;
using ShopMVC.Extentions;
using ShopMVC.ViewModel;

namespace ShopMVC.Services.Interface
{
        public interface IAccountServices
        {
            Task<User> SignInAsync(LoginViewModel user);
            Task<User> FindByEmail(string email);
            User SignUp(SignUpViewModel user);
            Task<string> Verification(string token);

    }
}
