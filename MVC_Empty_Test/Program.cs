namespace MVC_Empty_Test
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            //app.MapGet("/", () => "Hello World!");
            app.UseHttpsRedirection();
            app.UseRouting();

            //app.MapGet("/", async context =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});

            app.MapGet("/products", async context =>
            {
                await context.Response.WriteAsync("Products Page Content");
            });

            app.MapGet("/product/{id:int}", async context =>
            {
                if (int.TryParse(context.Request.RouteValues["id"]?.ToString(), out int id))
                {
                    await context.Response.WriteAsync($"Product {id} Details Page Content");
                }
                else
                {
                    await context.Response.WriteAsync("Invalid id");
                }
            });

            app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}",
            defaults: new { controller = "Home", action = "Index" }
            );

            app.MapControllerRoute(
            name: "about-us",
            pattern: "about-us",
            defaults: new { controller = "Home", action = "About" }
            );

            // page not found route handler
            app.Use(async (context, next) =>
            {
                await next();

                if (context.Response.StatusCode == 404)
                {
                    await context.Response.WriteAsync("Page not found");
                }
            });
            app.Run();
        }
    }
}
