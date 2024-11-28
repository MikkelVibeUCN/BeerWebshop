using BeerWebshop.APIClientLibrary.ApiClient.Client;
using BeerWebshop.APIClientLibrary.ApiClient.Client.Interfaces;
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

            ApplicationConfiguration.Initialize();

            using (var loginForm = new LoginForm())
            {
                if (loginForm.ShowDialog() == DialogResult.OK)
                {
                    Application.Run(new MainForm(loginForm.JwtToken));
                }
                else
                {
                    Application.Exit();
                }
            }
        }

        
        private static void ConfigureServices(ServiceCollection services)
        {

            services.AddTransient<IOrderApiClient, OrderApiClient>();
            services.AddTransient<OrderController>();
            services.AddTransient<MainForm>();
        }
    }
}