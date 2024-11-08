using BeerWebshop.DAL.DATA.DAO.DAOClasses;
using BeerWebshop.DAL.DATA.DAO.Interfaces;

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
