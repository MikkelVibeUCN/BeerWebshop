using BeerWebshop.Web.ApiClient;
using BeerWebshop.Web.Services;

namespace BeerWebshop.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            IRestClient restClient = new RestClientStub();
            builder.Services.AddSingleton(restClient);

            var beerService = new BeerService(restClient);
            builder.Services.AddSingleton(beerService);

            ICartService cartService = new CartServiceStop(beerService);
            builder.Services.AddSingleton(cartService);

            

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
        }
    }
}
