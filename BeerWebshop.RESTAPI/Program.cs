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

            builder.Services.AddScoped<IOrderService>(provider =>
            {
				return new OrderService(provider.GetRequiredService<IOrderDAO>(), provider.GetRequiredService<IProductDAO>(), connectionString);
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
