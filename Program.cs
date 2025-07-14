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
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            // Set up dependency injection for all services and controllers
            var host = CreateHostBuilder().Build();

            // Show login form and initialize database
            using (var scope = host.Services.CreateScope())
            {
                var pinService = scope.ServiceProvider.GetRequiredService<IPinService>();
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                
                // Initialize database synchronously
                DatabaseService.InitializeDatabaseAsync(context).Wait();
                
                // Show login form
                using (var loginForm = new LoginForm(pinService))
                {
                    if (loginForm.ShowDialog() != DialogResult.OK)
                    {
                        // User cancelled or failed authentication
                        return;
                    }
                }
            }

            // Run the application with controllers
            using (var scope = host.Services.CreateScope())
            {
                var machineController = scope.ServiceProvider.GetRequiredService<IMachineController>();
                var pinService = scope.ServiceProvider.GetRequiredService<IPinService>();
                Application.Run(new SelectMachineForm(machineController, pinService));
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
                    services.AddScoped<IMaintenanceRepository, MaintenanceRepository>();
                    services.AddScoped<IAlertRepository, AlertRepository>();
                    
                    // Add PIN service
                    services.AddSingleton<IPinService, PinService>();
                    
                    // Add MaintenanceController with ApplicationDbContext and MaintenanceRepository dependencies
                    services.AddScoped<IMaintenanceController>(provider => 
                    {
                        var context = provider.GetRequiredService<ApplicationDbContext>();
                        var maintenanceRepository = provider.GetRequiredService<IMaintenanceRepository>();
                        return new MaintenanceController(context, maintenanceRepository);
                    });
                    
                    // Add AlertController with ApplicationDbContext and AlertRepository dependencies
                    services.AddScoped<IAlertController>(provider => 
                    {
                        var context = provider.GetRequiredService<ApplicationDbContext>();
                        var alertRepository = provider.GetRequiredService<IAlertRepository>();
                        return new AlertController(context, alertRepository);
                    });
                    
                    // Add MachineController with ApplicationDbContext, MachineRepository, and AlertRepository dependencies
                    services.AddScoped<IMachineController>(provider => 
                    {
                        var context = provider.GetRequiredService<ApplicationDbContext>();
                        var machineRepository = provider.GetRequiredService<IMachineRepository>();
                        var alertRepository = provider.GetRequiredService<IAlertRepository>();
                        return new MachineController(context, machineRepository, alertRepository);
                    });
                });
        }
    }
}