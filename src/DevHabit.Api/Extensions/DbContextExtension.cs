namespace DevHabit.Api.Extensions;

using Common;
using Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

public static class DbContextExtension
{
    public static void AddDevHabitDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString(GlobalConstants.DbConnectionName) ?? string.Empty;
        services.AddDbContext<ApplicationDbContext>(options =>
            options
                .UseNpgsql(
                    connectionString, 
                    opt => opt.MigrationsHistoryTable(HistoryRepository.DefaultTableName, SchemaConstants.Application))
                .UseSnakeCaseNamingConvention());
    }
}
