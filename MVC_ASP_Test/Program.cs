using Company.Repository;
using Company.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Company.Database.Access.Contexts;
using Company.Service.Interfaces;
using Company.Service.Services;
using Company.Database.Access;
using Microsoft.AspNetCore.Identity;

namespace MVC_ASP_Test
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();


            builder.Services.AddDbContext<NorthwindContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped<NorthwindContextProcedures>();
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            //builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(
                config =>
                //add custom configurations here
                {
                    config.Password.RequiredUniqueChars = 1;
                    config.User.RequireUniqueEmail = true;
                    config.Lockout.AllowedForNewUsers = true;
                    config.Lockout.MaxFailedAccessAttempts = 3;
                    config.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(1);
                }
            ).AddEntityFrameworkStores<NorthwindContext>().
            AddDefaultTokenProviders();


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
                pattern: "{controller=Home}/{action=Index}");

            app.MapControllerRoute(
                name: "about-us",
                pattern: "{controller=About}/{action=Index}");

            app.Run();
        }
    }
}