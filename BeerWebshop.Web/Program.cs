using BeerWebshop.APIClientLibrary.ApiClient.Client;
using BeerWebshop.APIClientLibrary.ApiClient.Client.Interfaces;
using BeerWebshop.Web.Services;
using BeerWebshop.Web.Filter;
using BeerWebshop.Web.Properties;


namespace BeerWebshop.Web
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddControllersWithViews();

			string uri = "https://localhost:7244/api/v1/";

            builder.Services.AddScoped<AgeVerificationFilter>();

            builder.Services.AddControllersWithViews(options =>
            {
                options.Filters.Add<AgeVerificationFilter>(); // Adds the filter globally
            });

            builder.Services.Configure<BeerWebshop.Web.Properties.JwtSettings>(
				builder.Configuration.GetSection("JwtSettings"));

            // Register API clients with the base URI
            builder.Services.AddScoped<IProductAPIClient>(provider => new ProductAPIClient(uri));
            builder.Services.AddScoped<ICategoryAPIClient>(provider => new CategoryAPIClient(uri));
            builder.Services.AddScoped<IOrderApiClient>(provider => new OrderApiClient(uri));
            builder.Services.AddScoped<IAccountAPIClient>(provider => new AccountAPIClient(uri));
			// Register HttpContextAccessor for CookieService and other services
			builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddScoped<JWTService>();


            // Register application services
            builder.Services.AddScoped<ProductService>();
            builder.Services.AddScoped<CookieService>();
            builder.Services.AddScoped<ICartService, CartService>();
            builder.Services.AddScoped<CheckoutService>();
            builder.Services.AddScoped<OrderService>();
            builder.Services.AddScoped<AgeVerifierService>();
			builder.Services.AddScoped<AccountService>();


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
