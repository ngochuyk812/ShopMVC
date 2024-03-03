using Microsoft.Extensions.DependencyInjection;
using ShopMVC.Extentions;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
services.AddDbContext(builder.Configuration.GetConnectionString("Default") ?? "");
services.AddControllersWithViews();

var app = builder.Build();


app.UseStaticFiles();
app.UseRouting();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
