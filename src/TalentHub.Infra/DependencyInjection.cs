using Amazon.S3;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Npgsql;
using StackExchange.Redis;
using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Ports;
using TalentHub.Infra.Cache;
using TalentHub.Infra.Data;
using TalentHub.Infra.Data.Interceptors;
using TalentHub.Infra.Files;
using TalentHub.Infra.Security;
using TalentHub.Infra.Security.Options;
using TalentHub.Infra.Security.Services;
using TalentHub.Infra.SystemDateTime;

namespace TalentHub.Infra;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection s,
        IConfiguration config
    )
    {
        s.AddDbContext<TalentHubContext>(opt =>
        {
            NpgsqlConnectionStringBuilder builder = new()
            {
                Port = int.Parse(config["DB_PORT"] ?? throw new Exception("DB_PORT is required")),
                Host = config["DB_HOST"] ?? throw new Exception("DB_HOST is required"),
                Password = config["DB_PASSWORD"] ?? throw new Exception("DB_PASSWORD is required"),
                Database = config["DB_NAME"] ?? throw new Exception("DB_NAME is required"),
                Username = config["DB_USER"] ?? throw new Exception("DB_USER is required")
            };

            opt.UseNpgsql(builder.ToString()).UseSnakeCaseNamingConvention();

            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") != "Production")
            {
                opt.EnableDetailedErrors();
                opt.EnableSensitiveDataLogging();
            }
        });
        s.AddMemoryCache();
        s.AddHttpContextAccessor();
        s.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        s.AddScoped<IFileStorage, MinIoFileStorage>();
        s.AddScoped<IUserContext, HttpUserContext>();
        s.AddSingleton<IPasswordHasher, PasswordHasher>();
        s.AddSingleton<ITokenProvider, TokenProvider>();
        s.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        s.ConfigureOptions<AuthOptionsSetup>();
        s.AddAuthentication().AddJwtBearer();
        s.AddAuthorization();
        s.AddLogging(builder =>
        {
            builder.AddConsole();
            builder.AddDebug();
        });
        s.Configure<JwtOptions>((opt) =>
        {
            opt.Issuer = config["JWT_ISSUER"] ?? throw new Exception("JWT_ISSUER is required");
            opt.Audience = config["JWT_AUDIENCE"] ?? throw new Exception("JWT_AUDIENCE is required");
            opt.SecretKey = config["JWT_SECRET_KEY"] ?? throw new Exception("JWT_SECRET_KEY is required");
            opt.AccessTokenExpiration = int.Parse(config["JWT_ACCESS_TOKEN_EXPIRATION"] ?? throw new Exception("JWT_ACCESS_TOKEN_EXPIRATION is required"));
            opt.RefreshTokenExpiration = int.Parse(config["JWT_REFRESH_TOKEN_EXPIRATION"] ?? throw new Exception("JWT_REFRESH_TOKEN_EXPIRATION is required"));
        });

        string redisHost = config["REDIS_HOST"] ?? throw new Exception("REDIS_HOST is required");
        string redisPort = config["REDIS_PORT"] ?? throw new Exception("REDIS_PORT is required");
        string redisPass = config["REDIS_PASSWORD"] ?? throw new Exception("REDIS_PASSWORD is required");
        var options = new ConfigurationOptions
        {
            EndPoints = { $"{redisHost}:{redisPort}" },
            Password = redisPass,
            DefaultDatabase = 0
        };

        s.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(options));
        s.AddSingleton<ICacheProvider, RedisCacheProvider>();

        s.AddSingleton<IAmazonS3>(
            new AmazonS3Client(
                config["BUCKET_USER"] ?? throw new Exception("BUCKET_USER is required"),
                config["BUCKET_PASSWORD"] ?? throw new Exception("BUCKET_PASSWORD is required"),
                new AmazonS3Config
                {
                    ServiceURL = $"http://localhost:{config["BUCKET_PORT"] ?? throw new Exception("BUCKET_PORT is required")}",
                    ForcePathStyle = true
                }
            )
        );
        return s;
    }
}
