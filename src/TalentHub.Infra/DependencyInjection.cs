using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TalentHub.Infra.Data;

namespace TalentHub.Infra;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<TalentHubContext>(config =>
        {
            config.UseNpgsql(configuration.GetConnectionString("DefaultConnection")); 
        });
        return services;
    }
}