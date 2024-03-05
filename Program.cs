using Microsoft.Extensions.DependencyInjection;
using ShopMVC.Database.Model;
using ShopMVC.Extentions;
using ShopMVC.Repositories;
using ShopMVC.Repositories.Interface;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
services.AddDbContext(builder.Configuration.GetConnectionString("Default") ?? "");
services.AddControllersWithViews();
services.AddTransient<UnitOfWork>();
services.AddServices();
var app = builder.Build();


app.UseStaticFiles();
app.UseRouting();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
