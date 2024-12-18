using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Ports;
using TalentHub.Infra.Cache;
using TalentHub.Infra.Data;
using TalentHub.Infra.Files;
using TalentHub.Infra.Security;
using TalentHub.Infra.Security.Services;
using TalentHub.Infra.SystemDateTime;

namespace TalentHub.Infra;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, 
        IConfiguration configuration
    )   
    {
        services.AddDbContext<TalentHubContext>(config =>
        {
            config
                .UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
                .UseSnakeCaseNamingConvention();

            config.EnableSensitiveDataLogging();
            config.EnableDetailedErrors();
        });
        
        services.AddMemoryCache();
        services.AddHttpContextAccessor();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<ICacheProvider, InMemoryCacheProvider>();
        services.AddScoped<IFileStorage, MinIoFileStorage>();
        services.AddScoped<IUserContext, HttpUserContext>();
        services.AddSingleton<IPasswordHasher, PasswordHasher>();
        services.AddSingleton<ITokenProvider, TokenProvider>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        services.ConfigureOptions<AuthOptionsSetup>();
        services.AddAuthentication().AddJwtBearer();
        services.AddAuthorization();

        services.AddLogging(builder =>
        {
            builder.AddConsole();
            builder.AddDebug();
        });
        
        return services;
    }
}
