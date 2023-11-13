using ExamSystem.Domain.Entities.Identity;
using ExamSystem.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ExamSystem.API.Extensions
{
    public static class UpdateDatabaseExtension
    {
        public static async Task UpdateDatabase(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

            var loggerFactory = scope.ServiceProvider.GetRequiredService<ILoggerFactory>();

            try
            {
                await dbContext.Database.MigrateAsync();

                await AppContextSeed.AddSeedsAsync(dbContext, roleManager, userManager);
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "Database updating failed !");
            }
        }
    }
}
