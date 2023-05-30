using Microsoft.EntityFrameworkCore;
using MvcEc2Cubos.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string dbCubos = builder.Configuration.GetConnectionString("DbCubos");


builder.Services.AddDbContext<CubosContext>(options => {

    options.UseMySql(dbCubos, ServerVersion.AutoDetect(dbCubos));

});

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
