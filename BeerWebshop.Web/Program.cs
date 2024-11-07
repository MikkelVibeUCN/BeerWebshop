using BeerWebshop.APIClientLibrary.ApiClient.Client;
using BeerWebshop.Web.Services;
using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using BeerWebshop.APIClientLibrary.ApiClient;


namespace BeerWebshop.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            IProductAPIClient productAPIClient = new ProductApiClientStub();

            builder.Services.AddScoped<BeerService>(provider => new BeerService(productAPIClient));
            builder.Services.AddScoped<ICartService, CartService>();
            builder.Services.AddScoped<CookieService>();
            builder.Services.AddScoped<CheckoutService>();
            builder.Services.AddScoped<OrderService>();
            builder.Services.AddScoped<IOrderApiClient, OrderAPIClientStub>();

            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

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
