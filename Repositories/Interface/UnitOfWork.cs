using ShopMVC.Database;
using ShopMVC.Database.Model;

namespace ShopMVC.Repositories.Interface
{
    public class UnitOfWork
    {
        private ShopContext _context;
        public readonly IRepository<Product> Product;
        public readonly IRepository<Category> Category;
        public readonly IRepository<User> User;
        public readonly IRepository<Role> Role;
        public readonly IRepository<UserRole> UserRole;
        public readonly IRepository<Address> Address;
        public readonly IRepository<Review> Review;
        public readonly IRepository<MediaReview> MediaReview;
        public readonly IRepository<ImportProduct> ImportProduct;
        public readonly IRepository<Cart> Cart;


        public UnitOfWork(ShopContext context) {
            _context = context;
            Product = new Repository<Product>(_context);
            Category = new Repository<Category>(_context);
            User = new Repository<User>(_context);
            Role = new Repository<Role>(_context);
            UserRole = new Repository<UserRole>(_context);
            Address = new Repository<Address>(_context);
            Review = new Repository<Review>(_context);
            MediaReview = new Repository<MediaReview>(_context);
            ImportProduct = new Repository<ImportProduct>(_context);
            Cart = new Repository<Cart>(_context);
        }

        public void Commit()
        {
            _context.SaveChanges();

        }
        public async Task CommitAsync()
        {
           await _context.SaveChangesAsync();
        }
    }
}
