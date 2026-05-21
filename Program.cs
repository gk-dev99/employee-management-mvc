using EmployeeManagementSystem.Data;
using EmployeeManagementSystem.Services;
using Microsoft.EntityFrameworkCore;

//Creates ASP.NET Core application builder and loads configuration, logging and DI container
var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
//Registers MVC Services (controllers + Views Support)
builder.Services.AddControllersWithViews();

//Added by Gurmeet on 30-04-2026
//Reads connection string from configuration sources (appsettings, env vars, Azure settings etc.)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

//Registers AppDbContext in DI container and configures SQL Server provider
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

//Added by Gurmeet (DI)
//Registers EmployeeService with scoped lifetime for dependency injection
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

//Builds the application and finalizes service configuration
var app = builder.Build();

//middleware pipeline starts here
// Configure the HTTP request pipeline.
//Production exception handling middleware
if (!app.Environment.IsDevelopment())
{
    //Redirects unhandled exceptions to generic error page
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    //Forces browser to use HTTPS only
    app.UseHsts();
}

//Redirects HTTP requests to HTTPS
app.UseHttpsRedirection();

//Enables endpoint routing system
app.UseRouting();

//Enables authorization middleware
app.UseAuthorization();

//app.MapStaticAssets();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}")
//    .WithStaticAssets();

//added due to dotnet 8.0

//Serves static files from wwwroot folder (css,js,images)
app.UseStaticFiles();

//Defines default MVC route pattern
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Employee}/{action=Index}/{id?}");

//Starts the application and begins listening for HTTP requests
app.Run();
