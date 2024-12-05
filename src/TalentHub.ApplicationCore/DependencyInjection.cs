using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using TalentHub.ApplicationCore.Behaviors;

namespace TalentHub.ApplicationCore;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationCore(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            config.AddOpenBehavior(typeof(CacheBehavior<,>));
        });

        services.AddValidatorsFromAssembly(
            typeof(DependencyInjection).Assembly
        );

        return services;
    }
}
