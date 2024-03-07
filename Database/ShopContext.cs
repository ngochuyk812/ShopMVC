using Microsoft.EntityFrameworkCore;
using ShopMVC.Database.Model;
using System.Reflection.Metadata;

namespace ShopMVC.Database
{
    public class ShopContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<ImageProduct> ImageProducts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ImportProduct> ImportProducts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<MediaReview> MediaReviews { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public ShopContext(DbContextOptions<ShopContext> options): base(options)
        {
        }

    }
}
