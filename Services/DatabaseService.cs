using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using goodwin_winForm.Models;

namespace goodwin_winForm.Services
{
    /// <summary>
    /// Static service class responsible for database configuration and initialization.
    /// Provides methods for setting up Entity Framework services and initializing the database.
    /// </summary>
    public static class DatabaseService
    {
        /// <summary>
        /// Configures Entity Framework services for the application using the specified connection string.
        /// This method sets up the database context with SQL Server provider and optimized settings.
        /// </summary>
        /// <param name="services">The service collection to add database services to.</param>
        /// <param name="connectionString">The connection string for the database.</param>
        /// <returns>The service collection with database services configured.</returns>
        /// <remarks>
        /// This method is used during application startup to configure Entity Framework.
        /// It registers the ApplicationDbContext with the dependency injection container.
        /// The connection string should point to a valid SQL Server instance.
        /// </remarks>
        public static IServiceCollection AddDatabaseServices(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString, sqlOptions =>
                {
                    // Enable connection pooling for better performance
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 3,
                        maxRetryDelay: TimeSpan.FromSeconds(5),
                        errorNumbersToAdd: null);
                    
                    // Optimize for read-heavy workloads
                    sqlOptions.CommandTimeout(30);
                }));

            return services;
        }

        /// <summary>
        /// Initializes the database asynchronously, ensuring it exists and is up to date.
        /// This method creates the database if it doesn't exist and applies any pending migrations.
        /// </summary>
        /// <param name="context">The database context to initialize.</param>
        /// <remarks>
        /// This method performs the following operations:
        /// - Ensures the database exists (creates it if necessary)
        /// - Applies any pending Entity Framework migrations
        /// - Logs success or failure messages to the console
        /// 
        /// This method should be called during application startup to ensure
        /// the database is properly configured before the application begins.
        /// </remarks>
        public static async Task InitializeDatabaseAsync(ApplicationDbContext context)
        {
            try
            {
                // Check if database exists first to avoid unnecessary operations
                if (!await context.Database.CanConnectAsync())
                {
                    // Database doesn't exist, create it
                    await context.Database.EnsureCreatedAsync();
                    Console.WriteLine("Database created successfully.");
                }
                else
                {
                    // Database exists, just check for migrations
                    var pendingMigrations = context.Database.GetPendingMigrations().ToList();
                    if (pendingMigrations.Any())
                    {
                        await context.Database.MigrateAsync();
                        Console.WriteLine($"Applied {pendingMigrations.Count} migrations successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Database is up to date.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error initializing database: {ex.Message}");
                // Don't throw the exception to prevent app crash, just log it
                // The app can still work with a basic setup
            }
        }

        /// <summary>
        /// Gets the default connection string for local development.
        /// This method provides a connection string that works with LocalDB.
        /// </summary>
        /// <returns>A connection string for the local development database.</returns>
        /// <remarks>
        /// Returns a connection string configured for:
        /// - LocalDB instance (local development)
        /// - Database name: "GoodwinMachineMonitoring"
        /// - Trusted connection (Windows authentication)
        /// - Multiple active result sets enabled
        /// 
        /// This connection string is suitable for development and testing.
        /// For production, use a different connection string with proper security.
        /// </remarks>
        public static string GetDefaultConnectionString()
        {
            // Default connection string for local development
            return "Server=(localdb)\\mssqllocaldb;Database=GoodwinMachineMonitoring;Trusted_Connection=true;MultipleActiveResultSets=true";
        }
    }
} 