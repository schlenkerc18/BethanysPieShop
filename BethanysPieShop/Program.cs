using BethanysPieShop.Domain.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ICategoryRepository, MockCategoryRepository>();
builder.Services.AddScoped<IPieRepository, MockPieRepository>();

builder.Services.AddControllersWithViews(); // makes sure asp.net core app will know about mvc

var app = builder.Build();

app.UseStaticFiles();  // look in wwwroot folder for static files, shortcircuit request

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.MapDefaultControllerRoute(); // lets us see our pages

app.Run(); // starts app
