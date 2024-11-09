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

            builder.Services.AddScoped<IProductDAO>(provider =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                return new ProductDAO(connectionString);
            });

            builder.Services.AddScoped<IOrderDAO>(provider =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                return new OrderDao(connectionString);
            });

            builder.Services.AddScoped<ICategoryDAO>(provider =>
			{
				var configuration = provider.GetRequiredService<IConfiguration>();
				var connectionString = configuration.GetConnectionString("DefaultConnection");
				return new CategoryDAO(connectionString);
			});

			builder.Services.AddScoped<IBreweryDAO>(provider =>
			{
				var configuration = provider.GetRequiredService<IConfiguration>();
				var connectionString = configuration.GetConnectionString("DefaultConnection");
				return new BreweryDAO(connectionString);
			});

            builder.Services.AddScoped<CategoryService>(provider => new CategoryService(provider.GetRequiredService<ICategoryDAO>()));
            builder.Services.AddScoped<BreweryService>(provider => new BreweryService(provider.GetRequiredService<IBreweryDAO>()));
            builder.Services.AddScoped<ProductService>(provider => new ProductService(provider.GetRequiredService<IProductDAO>()));



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
