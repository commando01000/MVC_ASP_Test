using Company.Database.Access;
using Company.Database.Access.Contexts;
using Company.Repository;
using Company.Repository.Interfaces;
using Company.Service.Helper;
using Company.Service.Interfaces;
using Company.Service.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MVC_ASP_Test.ApplicationServices;

namespace MVC_ASP_Test
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;
            // Add services to the container.
            builder.Services.AddControllersWithViews();


            builder.Services.AddDbContext<NorthwindContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped<NorthwindContextProcedures>();

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

            builder.Services.AddApplicationServices(builder);

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/SignOut";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.Cookie.Name = "MVC_ASP_Test";
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(40);
                options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                options.Cookie.SameSite = SameSiteMode.Strict;
            });


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

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=SignIn}");

            app.MapControllerRoute(
                name: "about-us",
                pattern: "{controller=About}/{action=Index}");

            app.MapControllerRoute(
                name: "sign-up",
                pattern: "{controller=Account}/{action=SignUp}");
            app.Run();
        }
    }
}