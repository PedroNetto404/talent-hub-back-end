using Microsoft.EntityFrameworkCore;

namespace TalentHub.Infra.Data;

public sealed class TalentHubContext(DbContextOptions<TalentHubContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TalentHubContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}