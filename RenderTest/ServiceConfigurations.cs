namespace RenderTest;

using Microsoft.EntityFrameworkCore;
using RenderTest.Abstractions.Services;
using RenderTest.Data;
using RenderTest.Services;
using StackExchange.Redis;

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

        builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
        {
            var configuration = ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("redis"));
            configuration.AbortOnConnectFail = false;
            configuration.ConnectRetry = 5;
            configuration.ConnectTimeout = 20000;
            configuration.SyncTimeout = 20000;

            return ConnectionMultiplexer.Connect(configuration);
        });

        builder.Services.AddScoped<IRedisService, RedisService>();
    }
}
