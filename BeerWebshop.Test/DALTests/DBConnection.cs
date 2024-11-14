using Microsoft.Extensions.Configuration;

namespace BeerWebshop.Test.DALTests;

public static class DBConnection
{

    public static string ConnectionString()
    {
		var configuration = new ConfigurationBuilder()
	.SetBasePath(Directory.GetCurrentDirectory())
	.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
	.AddUserSecrets<ProductDaoTests>()
	.Build();

		// Retrieve the connection string from user secrets
		var connectionString = configuration.GetConnectionString("BeerWebshop");

		if (string.IsNullOrEmpty(connectionString))
		{
			throw new InvalidOperationException("Connection string 'BeerWebshop' was not found in user secrets or appsettings.json.");
		}

		return connectionString;
	}
}