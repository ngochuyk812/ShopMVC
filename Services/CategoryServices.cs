using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ShopMVC.Database.Model;
using ShopMVC.Extentions;
using ShopMVC.Repositories.Interface;
using ShopMVC.Services.Interface;
using ShopMVC.Settings;
using ShopMVC.ViewModel;

namespace ShopMVC.Services
{
    public class CategoryServices : ICategoryServices
    {
        public UnitOfWork unitOfWork { get; set; }

        public CategoryServices(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Category>> ListAsync()
        {
            var rs = await unitOfWork.Category.FindListAsync(f=>true);
            return rs;
        }
    }
}
