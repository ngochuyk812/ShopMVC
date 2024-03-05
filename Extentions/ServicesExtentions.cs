using Microsoft.EntityFrameworkCore;
using ShopMVC.Database;
using ShopMVC.Services;
using ShopMVC.Services.Interface;

namespace ShopMVC.Extentions
{
    public static class ServicesExtentions
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
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IProductServices, ProductServices>();

            return services;
        }

    }
}
