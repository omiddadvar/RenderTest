namespace RenderTest;

using Microsoft.EntityFrameworkCore;
using RenderTest.Data;

public static class ServiceConfigurations
{
    public static void AddServiceConfigurations(this WebApplicationBuilder builder)
    {
        builder.AddDatabaseConfigurations();
        builder.AddRedisConfigurations();
    }
    private static void AddDatabaseConfigurations(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<MainDBContext>(option =>
        {
            option.UseNpgsql(
               builder.Configuration.GetConnectionString("postgres"),
               npgsqlOptions =>
               {
                   npgsqlOptions.EnableRetryOnFailure();
                   npgsqlOptions.MigrationsAssembly("RenderTest");
               }
           );
        }, 
        ServiceLifetime.Scoped);

    }
    private static void AddRedisConfigurations(this WebApplicationBuilder builder)
    {
        builder.Services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = builder.Configuration.GetConnectionString("redis");
            options.InstanceName = "RenderTest:"; // Optional: prefix for all keys
        });
    }
}
