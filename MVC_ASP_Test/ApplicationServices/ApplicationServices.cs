using Company.Repository.Interfaces;
using Company.Repository;
using Company.Service.Helper;
using Company.Service.Interfaces;
using Company.Service.Services;
using Microsoft.AspNetCore.Authentication.Google;

namespace MVC_ASP_Test.ApplicationServices
{
    public static class ApplicationServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, WebApplicationBuilder builder)
        {

            services.Configure<TwilioSettings>(builder.Configuration.GetSection("TwilioSettings"));
            services.AddScoped<ISMSService, SMSService>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = GoogleDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
            })
                .AddGoogle(googleOptions =>
            {
                googleOptions.ClientId = builder.Configuration["Authentication:Google:ClientId"];
                googleOptions.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
            });
            return services;
        }
    }
}
