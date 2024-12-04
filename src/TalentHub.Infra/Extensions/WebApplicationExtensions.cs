using System.Text.Json;
using Amazon.Runtime.Internal.Util;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TalentHub.ApplicationCore.Resources.Courses;
using TalentHub.ApplicationCore.Resources.Universities;
using TalentHub.Infra.Data;

namespace TalentHub.Infra.Extensions;

public static class WebApplicationExtensions
{
    public async static Task SeedDatabaseAsync(this WebApplication app)
    {
        await using AsyncServiceScope scope = app.Services.CreateAsyncScope();
        TalentHubContext context = scope.ServiceProvider.GetRequiredService<TalentHubContext>();
        ILogger<WebApplication> logger = scope.ServiceProvider.GetRequiredService<ILogger<WebApplication>>();

        logger.LogInformation("starting database seed");

        await SeedEntities(
            context,
            "universities_seed.json",
            p => University.Create(p).Value,
            u => u.Name,
            "universities",
            logger
        );

        await SeedEntities(
            context,
            "courses_seed.json",
            p => Course.Create(p).Value,
            c => c.Name,
            "courses",
            logger
        );

        Console.WriteLine("Database seeding process completed.\n");
    }

    private static async Task SeedEntities<TEntity>(
        TalentHubContext context,
        string seedFileName,
        Func<string, TEntity> createEntityFunc,
        Func<TEntity, string> getEntityKey,
        string entityName,
        ILogger<WebApplication> logger
    ) where TEntity : class
    {
        try
        {
            string seedFilePath = GetSeedFilePath(seedFileName);

            logger.LogInformation("Seeding {entityName} from {seedFilePath}", entityName, seedFilePath);
            if (!File.Exists(seedFilePath))
            {
                logger.LogError("{entityName} seed file not found. Skipping seeding process", entityName);
                return;
            }

            string fileContent = await File.ReadAllTextAsync(seedFilePath);
            if (string.IsNullOrWhiteSpace(fileContent))
            {
                logger.LogInformation("{entityName} seed is empty. Skipping seeding process.", entityName);
                return;
            }

            JsonElement[] entitiesToSeed = JsonSerializer.Deserialize<JsonElement[]>(fileContent)!;
            if (!entitiesToSeed.Any())
            {
                logger.LogInformation("No {entityName} to seed. Skipping seeding process.", entityName);
                return;
            }

            IEnumerable<string> entityKeysInDatabase =
                await context
                    .Set<TEntity>()
                    .ToListAsync()
                    .ContinueWith(task => task.Result.Select(getEntityKey));

            foreach (JsonElement entity in entitiesToSeed)
            {
                TEntity newEntity = createEntityFunc(entity.GetProperty("label").GetString()!);
                if (!entityKeysInDatabase.Contains(getEntityKey(newEntity)))
                {
                    await context.Set<TEntity>().AddAsync(newEntity);
                }
            }

            await context.SaveChangesAsync();

            logger.LogInformation("{entitiesToSeed.Length} {entityName} seeded successfully.", entitiesToSeed.Length, entityName);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error seeding {entityName}", entityName);
        }
    }

    private static string GetSeedFilePath(string fileName)
    {
        string basePath = Directory.GetCurrentDirectory();

#if DEBUG
        basePath = Path.Combine(
            basePath,
            "bin",
            "Debug",
            $"net{Environment.Version.Major}.{Environment.Version.Minor}");
#endif
        return Path.Combine(basePath, fileName);
    }
}
