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

        public ShopContext(DbContextOptions<ShopContext> options): base(options)
        {
        }

    }
}
