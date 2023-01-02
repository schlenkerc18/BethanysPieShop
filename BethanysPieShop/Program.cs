using BethanysPieShop.Data;
using BethanysPieShop.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IPieRepository, PieRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddScoped<IShoppingCart, ShoppingCart>(sp => ShoppingCart.GetCart(sp));
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

builder.Services.AddControllersWithViews()  // makes sure asp.net core app will know about mvc
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    }); 
builder.Services.AddRazorPages();
builder.Services.AddDbContext<BethanysPieShopDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("BethanysPieShopDb"));
});

builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddEntityFrameworkStores<BethanysPieShopDbContext>();

/*builder.Services.AddControllers();*/ // for just an api without views, but don't need this since we've already added controllers with views
builder.Services.AddServerSideBlazor(); // add blazor to application

var app = builder.Build();

app.UseStaticFiles();  // look in wwwroot folder for static files, shortcircuit request
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.MapDefaultControllerRoute(); // lets us see our pages. default route:  "{controller=Home}/{action=Index}/{id?}"
//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

//app.MapControllers(); use for an api, not required since we already included default routes

app.MapRazorPages();

app.MapBlazorHub(); // adding blazor stuff
app.MapFallbackToPage("/app/{*catchall}", "/App/Index");

DbInitializer.Seed(app);
app.Run(); // starts app
