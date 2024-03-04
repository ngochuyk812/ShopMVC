using ShopMVC.Database;
using ShopMVC.Database.Model;

namespace ShopMVC.Repositories.Interface
{
    public class UnitOfWork
    {
        private ShopContext _context;
        public readonly IRepository<Product> Product;

        public UnitOfWork(ShopContext context) {
            _context = context;
            Product = new Repository<Product>(_context);
        }
    }
}
