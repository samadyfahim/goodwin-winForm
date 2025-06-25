using goodwin_winForm.Forms;
using goodwin_winForm.Services;
using goodwin_winForm.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace goodwin_winForm
{
    /// <summary>
    /// Main application class that serves as the entry point for the Machine Management System.
    /// This class handles application startup, dependency injection setup, and form navigation.
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// This method initializes the application, sets up dependency injection,
        /// handles authentication, and starts the main application flow.
        /// </summary>
        /// <remarks>
        /// The application flow is as follows:
        /// 1. Initialize application configuration
        /// 2. Set up dependency injection container
        /// 3. Show login form for authentication
        /// 4. Initialize database if authentication succeeds
        /// 5. Show main machine selection form
        /// 
        /// If authentication fails or is cancelled, the application exits.
        /// </remarks>
        [STAThread]
        static async Task Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            // Set up dependency injection for all services and controllers
            var host = CreateHostBuilder().Build();

            // Show login form and initialize database in parallel for better performance
            using (var scope = host.Services.CreateScope())
            {
                var authController = scope.ServiceProvider.GetRequiredService<IAuthController>();
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                
                // Start database initialization in background
                var databaseTask = DatabaseService.InitializeDatabaseAsync(context);
                
                // Show login form
                using (var loginForm = new LoginForm(authController))
                {
                    if (loginForm.ShowDialog() != DialogResult.OK)
                    {
                        // User cancelled or failed authentication
                        return;
                    }
                }
                
                // Wait for database initialization to complete
                await databaseTask;
            }

            // Run the application with controllers
            using (var scope = host.Services.CreateScope())
            {
                var machineController = scope.ServiceProvider.GetRequiredService<IMachineController>();
                var authController = scope.ServiceProvider.GetRequiredService<IAuthController>();
                Application.Run(new SelectMachineForm(machineController, authController));
            }
        }

        /// <summary>
        /// Creates and configures the host builder with all necessary services and dependencies.
        /// This method sets up the dependency injection container with all required services.
        /// </summary>
        /// <returns>A configured host builder ready to build the application host.</returns>
        /// <remarks>
        /// This method configures the following services:
        /// - Database services with Entity Framework
        /// - Repository services for data access
        /// - PIN service for authentication
        /// - Controller services for business logic
        /// 
        /// All services are registered with appropriate lifetimes:
        /// - Scoped: Controllers and repositories (per request/scope)
        /// - Singleton: PIN service (application-wide)
        /// </remarks>
        public static IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    // Add database services
                    var connectionString = DatabaseService.GetDefaultConnectionString();
                    services.AddDatabaseServices(connectionString);
                    
                    // Add repository services
                    services.AddScoped<IMachineRepository, MachineRepository>();
                    
                    // Add PIN service
                    services.AddSingleton<IPinService, PinService>();
                    
                    // Add controllers
                    services.AddScoped<IAuthController, AuthController>();
                    services.AddScoped<IMachineController, MachineController>();
                });
        }
    }
}