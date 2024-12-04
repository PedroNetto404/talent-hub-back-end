using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TalentHub.Infra.Data;

public sealed class TalentHubContext(DbContextOptions<TalentHubContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TalentHubContext).Assembly);
        base.OnModelCreating(modelBuilder);

        IEnumerable<IMutableForeignKey> relations = 
            modelBuilder
                .Model
                .GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys()); 
        
        foreach (IMutableForeignKey relationship in  relations)
        {
            relationship.DeleteBehavior = DeleteBehavior.Cascade;
        }
    }
}
