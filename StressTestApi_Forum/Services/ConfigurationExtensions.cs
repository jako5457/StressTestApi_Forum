using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using StressTestApi_Forum.Services.Efcore;

namespace StressTestApi_Forum.Services
{
    public static class ConfigurationExtensions
    {

        public static WebApplicationBuilder AddForumDBContext(this WebApplicationBuilder application)
        {
            DatabaseSettings dbSettings = new DatabaseSettings();
            application.Configuration.GetSection("Database").Bind(dbSettings);

            application.Services.AddDbContext<ForumContext>(options =>
            {
                options.UseSqlServer(dbSettings.ConnectionString);
            });

            return application;
        }

        private class DatabaseSettings
        {
            public string ConnectionString { get; set; } = string.Empty;
        }

        public static async Task<WebApplication> CheckDbMigrationsAsync(this WebApplication application)
        {
            var scope = application.Services.CreateScope();

            var Context = scope.ServiceProvider.GetRequiredService<ForumContext>();

            application.Logger.LogInformation("Checking for pending migrations.");

            var pendingMigrations = await Context.Database.GetPendingMigrationsAsync();

            if (pendingMigrations.Any())
            {
                application.Logger.LogInformation("Updating Database.");
                await Context.Database.MigrateAsync();
            }
            else
            {
                application.Logger.LogInformation("No batabase upgrade reqired.");
            }

            return application;
        }

    }
}
