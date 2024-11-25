using BeerWebshop.DAL.DATA.DAO.DAOClasses;
using BeerWebshop.DAL.DATA.DAO.Interfaces;
using BeerWebshop.RESTAPI.Properties;
using BeerWebshop.RESTAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

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

			var jwtSettingsSection = builder.Configuration.GetSection("JwtSettings");

            builder.Services.Configure<JWTSettings>(jwtSettingsSection);

            builder.Services.AddScoped<JWTService>();

            builder.Services.AddScoped<AccountService>(provider => new AccountService(provider.GetRequiredService<IAccountDAO>(), provider.GetRequiredService<JWTService>()));

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



            var jwtSettings = jwtSettingsSection.Get<JWTSettings>();
            var key = Encoding.UTF8.GetBytes(jwtSettings.SecretKey);


            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .			AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					ValidIssuer = jwtSettings.Issuer,
					ValidAudience = jwtSettings.Audience,
					IssuerSigningKey = new SymmetricSecurityKey(key),
                    RoleClaimType = ClaimTypes.Role
                };
			});

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
                options.AddPolicy("UserOnly", policy => policy.RequireRole("User"));
            });


            var app = builder.Build();



			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

			app.Run();
		}
	}
}
