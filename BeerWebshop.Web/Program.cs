using BeerWebshop.APIClientLibrary.ApiClient.Client;
using BeerWebshop.APIClientLibrary.ApiClient.Client.Interfaces;
using BeerWebshop.Web.Services;


namespace BeerWebshop.Web
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddControllersWithViews();

			string uri = "https://localhost:7244/api/v1/";

			// Register API clients with the base URI
			builder.Services.AddSingleton<IProductAPIClient>(new ProductAPIClient(uri));
			builder.Services.AddSingleton<ICategoryAPIClient>(new CategoryAPIClient(uri));
			// Register HttpContextAccessor for CookieService and other services
			builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

			// Register application services
			builder.Services.AddScoped<BeerService>();
			builder.Services.AddScoped<CookieService>();
			builder.Services.AddScoped<ICartService, CartService>();
			builder.Services.AddScoped<CheckoutService>();
			builder.Services.AddScoped<OrderService>();
			// Add this line to register AgeVerifierService
			builder.Services.AddScoped<AgeVerifierService>();


			// Use a stub for the IOrderApiClient
			builder.Services.AddScoped<IOrderApiClient>(provider => new OrderApiClient(uri));


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
