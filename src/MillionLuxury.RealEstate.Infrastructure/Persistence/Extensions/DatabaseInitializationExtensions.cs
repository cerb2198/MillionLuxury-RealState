using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MillionLuxury.RealEstate.Infrastructure.Persistence.Databases;

namespace MillionLuxury.RealEstate.Infrastructure.Persistence.Extensions;

public static class DatabaseInitializationExtensions
{
    private const string RunMigrationsEnvVar = "RunMigrationsOnStartup";
    public static async Task InitializeDatabaseAsync(this IServiceProvider serviceProvider, IConfiguration configuration)
    {
        if (!configuration.GetValue<bool>(RunMigrationsEnvVar))
            return;

        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<RealEstateDbContext>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<RealEstateDbContext>>();

        try
        {
            logger.LogInformation("Starting automatic database migration...");

            var pendingMigrations = await context.Database.GetPendingMigrationsAsync();

            if (pendingMigrations.Any())
            {
                logger.LogInformation("Found {Count} pending migrations. Applying migrations...", pendingMigrations.Count());

                foreach (var migration in pendingMigrations)
                {
                    logger.LogInformation("Pending migration: {Migration}", migration);
                }

                await context.Database.MigrateAsync();

                logger.LogInformation("Database migration completed successfully.");
            }
            else
            {
                logger.LogInformation("No pending migrations found. Database is up to date.");
            }
        }
        catch (Exception ex)
        {
            logger.LogCritical(ex, "Database migration failed in production environment. Application will terminate.");
            throw;
        }
    }
}
