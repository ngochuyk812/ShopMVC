using ShopMVC.Extentions;
using ShopMVC.Repositories.Interface;
using ShopMVC.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using ShopMVC.Mapping;
using ShopMVC.Helper;


var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var Configuration = builder.Configuration;
services.Configure<MailSetting>(Configuration.GetSection("Mail"));
services.Configure<PaginationSetting>(Configuration.GetSection("Pagination"));
services.AddAuthentication("SecurityScheme")
    .AddCookie("SecurityScheme", options =>
    {
        options.AccessDeniedPath = new PathString("/Account/Access");
        options.Cookie = new CookieBuilder
        {
            HttpOnly = true,
            Name = ".aspNetCoreDemo.Security.Cookie",
            Path = "/",
            SameSite = SameSiteMode.Lax,
            SecurePolicy = CookieSecurePolicy.SameAsRequest
        };
        options.LoginPath = new PathString("/Account/Login");
        options.ReturnUrlParameter = "RequestPath";
        options.SlidingExpiration = true;
    });

services.AddDbContext(Configuration.GetConnectionString("Default") ?? "");
services.AddControllersWithViews();
services.AddAutoMapper
(typeof(AutoMapperProfile).Assembly);
services.AddSingleton<MailHelper>();
services.AddTransient<UnitOfWork>();
services.AddHttpContextAccessor();

services.AddServices();
var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
