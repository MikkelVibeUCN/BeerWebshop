using BeerWebshop.APIClientLibrary.ApiClient.Client.Interfaces;
using BeerWebshop.APIClientLibrary.ApiClient.Client;
using BeerWebshop.DesktopClient.Controllers;
using Microsoft.Extensions.DependencyInjection;

namespace BeerWebshop.DesktopClient
{
    internal static class Program
    {


        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }

        private static void ConfigureServices(ServiceCollection services)
		{

			services.AddTransient<IOrderApiClient, OrderApiClient>();
			services.AddTransient<OrderController>();
			services.AddTransient<MainForm>();
		}
	}
}