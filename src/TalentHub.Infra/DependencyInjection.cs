using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
            config.UseNpgsql(configuration.GetConnectionString("DefaultConnection")); 
        });

        services.AddScoped<IFileStorage, MinIOFileStorage>();
        return services;
    }
}