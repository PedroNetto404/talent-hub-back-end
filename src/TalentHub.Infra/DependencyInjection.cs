using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Ports;
using TalentHub.Infra.Data;
using TalentHub.Infra.File;

namespace TalentHub.Infra;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<TalentHubContext>(config =>
        {
            config
                .UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
                .UseSnakeCaseNamingConvention();

            config.EnableSensitiveDataLogging();
            config.EnableDetailedErrors();
        });

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IFileStorage, MinIOFileStorage>();
        return services;
    }
}