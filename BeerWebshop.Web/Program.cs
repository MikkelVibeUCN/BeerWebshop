using BeerWebshop.APIClientLibrary.ApiClient.Client;
using BeerWebshop.Web.Services;
using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using BeerWebshop.APIClientLibrary.ApiClient;
using static System.Net.WebRequestMethods;
using BeerWebshop.Web.Filter;


namespace BeerWebshop.Web
{
    public class Program
    {
        private static readonly string uri = "https://localhost:7244/api/v1/";
        public static void Main(string[] args)
        {



            var builder = WebApplication.CreateBuilder(args);

            
            builder.Services.AddSingleton<IProductAPIClient>(new ProductAPIClient(uri));
            builder.Services.AddSingleton<ICategoryAPIClient>(new CategoryAPIClient(uri));

            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Register application services
            builder.Services.AddScoped<BeerService>();
            builder.Services.AddScoped<CookieService>();
            builder.Services.AddScoped<AgeVerificationFilter>();
            builder.Services.AddScoped<AgeVerifierService>(); 
            builder.Services.AddScoped<ICartService, CartService>();
            builder.Services.AddScoped<CheckoutService>();
            builder.Services.AddScoped<OrderService>();

            // Use a stub for the IOrderApiClient
            builder.Services.AddSingleton<IOrderApiClient, OrderAPIClientStub>();

            builder.Services.AddControllersWithViews(options =>
            {
                options.Filters.AddService<AgeVerificationFilter>();
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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
