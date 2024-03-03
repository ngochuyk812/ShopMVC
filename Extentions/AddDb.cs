using Microsoft.EntityFrameworkCore;
using ShopMVC.Database;

namespace ShopMVC.Extentions
{
    public static class AddDb
    {
        public static IServiceCollection AddDbContext(this IServiceCollection services, string connectString)
        {
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 36));
            services.AddDbContext<ShopContext>(
                dbContextOptions => dbContextOptions
                    .UseMySql(connectString, serverVersion)
                    .LogTo(Console.WriteLine, LogLevel.Information)
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors()
            );
            return services;
        }
    }
}
