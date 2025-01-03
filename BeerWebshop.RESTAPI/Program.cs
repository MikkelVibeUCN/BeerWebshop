using BeerWebshop.DAL.DATA.DAO.DAOClasses;
using BeerWebshop.DAL.DATA.DAO.Interfaces;
using BeerWebshop.RESTAPI.Properties;
using BeerWebshop.RESTAPI.Services;
using BeerWebshop.RESTAPI.Services.Interfaces;
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

            builder.Services.AddScoped<IOrderDAO>(provider => new OrderDAO(connectionString));
            builder.Services.AddScoped<IProductDAO>(provider => new ProductDAO(connectionString));
            builder.Services.AddScoped<IBreweryDAO>(provider => new BreweryDAO(connectionString));
            builder.Services.AddScoped<ICategoryDAO>(provider => new CategoryDAO(connectionString));
            builder.Services.AddScoped<IAccountDAO>(provider => new AccountDAO(connectionString));

            builder.Services.AddScoped<ICategoryService>(provider =>
                new CategoryService(provider.GetRequiredService<ICategoryDAO>()));
            builder.Services.AddScoped<IBreweryService>(provider =>
                new BreweryService(provider.GetRequiredService<IBreweryDAO>()));
            builder.Services.AddScoped<IProductService>(provider =>
                new ProductService(
                    provider.GetRequiredService<IProductDAO>(),
                    provider.GetRequiredService<ICategoryService>(),
                    provider.GetRequiredService<IBreweryService>()
                ));


            var jwtSettingsSection = builder.Configuration.GetSection("JwtSettings");

            builder.Services.Configure<JWTSettings>(jwtSettingsSection);

            builder.Services.AddScoped<JWTService>();

            builder.Services.AddScoped<IAccountService>(provider => new AccountService(provider.GetRequiredService<IAccountDAO>(), provider.GetRequiredService<JWTService>()));

            builder.Services.AddScoped<IOrderService>(provider =>
            {
                return new OrderService(
                    provider.GetRequiredService<IOrderDAO>(),
                    provider.GetRequiredService<IProductService>(),
                    connectionString);
            });

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var jwtSettings = jwtSettingsSection.Get<JWTSettings>();
            var key = Encoding.UTF8.GetBytes(jwtSettings.SecretKey);

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
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