using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using TalentHub.Infra.Data.Interceptors;

namespace TalentHub.Infra.Data;

public sealed class TalentHubContext(DbContextOptions<TalentHubContext> options) : DbContext(options)
{
    private static readonly IInterceptor versionInterceptor = new VersionedEntityInterceptor(); 

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TalentHubContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(versionInterceptor);

        base.OnConfiguring(optionsBuilder);
    }
}
