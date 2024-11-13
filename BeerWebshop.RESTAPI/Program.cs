using BeerWebshop.DAL.DATA.DAO.DAOClasses;
using BeerWebshop.DAL.DATA.DAO.Interfaces;
using BeerWebshop.RESTAPI.Services;

namespace BeerWebshop.RESTAPI
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

			builder.Services.AddScoped<IOrderDAO>(_ => new OrderDAO(connectionString));
			builder.Services.AddScoped<IProductDAO>(_ => new ProductDAO(connectionString));
			builder.Services.AddScoped<IBreweryDAO>(_ => new BreweryDAO(connectionString));
			builder.Services.AddScoped<ICategoryDAO>(_ => new CategoryDAO(connectionString));
            builder.Services.AddScoped<IAccountDAO>(_ => new AccountDAO(connectionString));


            builder.Services.AddScoped<CategoryService>(provider => new CategoryService(provider.GetRequiredService<ICategoryDAO>()));
			builder.Services.AddScoped<BreweryService>(provider => new BreweryService(provider.GetRequiredService<IBreweryDAO>()));
			builder.Services.AddScoped<ProductService>(provider =>
				new ProductService(
					provider.GetRequiredService<IProductDAO>(),
					provider.GetRequiredService<CategoryService>(),
					provider.GetRequiredService<BreweryService>()
				));

			builder.Services.AddScoped<OrderService>(provider =>
			{
				return new OrderService(
					provider.GetRequiredService<IOrderDAO>(),
					provider.GetRequiredService<ProductService>(),
					connectionString);
			});



			builder.Services.AddControllers();
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();


			var app = builder.Build();



			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
