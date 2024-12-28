using Amazon.S3;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using StackExchange.Redis;
using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Ports;
using TalentHub.Infra.Adapters;
using TalentHub.Infra.Cache;
using TalentHub.Infra.Data;
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
        s.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        s.AddScoped(
            typeof(IResourceOwnershipValidator<>), 
            typeof(ResourceOwnershipValidatorAdapter<>)
        );
        s.AddScoped<IUserContext, HttpUserContext>();
        s.AddSingleton<IHasher, Sha256Hasher>();
        s.AddSingleton<ITokenProvider, TokenProvider>();
        s.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        s.AddMemoryCache();
        s.AddHttpContextAccessor();

        AddDatabase(s, config);
        AddAuth(s, config);
        AddCache(s, config);
        AddBucket(s, config);

        return s;
    }

    private static void AddBucket(IServiceCollection s, IConfiguration config)
    {
        s.AddScoped<IFileStorage, MinIoFileStorage>();
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
    }

    private static void AddDatabase(IServiceCollection s, IConfiguration config)
    {
        s.AddDbContext<TalentHubContext>(opt =>
        {
            NpgsqlConnectionStringBuilder builder = new()
            {
                Port = int.Parse(config["DB_PORT"]
                ?? throw new Exception("DB_PORT is required")),

                Host = config["DB_HOST"]
                ?? throw new Exception("DB_HOST is required"),

                Password = config["DB_PASSWORD"]
                ?? throw new Exception("DB_PASSWORD is required"),

                Database = config["DB_NAME"]
                ?? throw new Exception("DB_NAME is required"),

                Username = config["DB_USER"]
                ?? throw new Exception("DB_USER is required")
            };

            opt
                .UseNpgsql(builder.ToString())
                .UseSnakeCaseNamingConvention();

            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") != "Production")
            {
                opt.EnableDetailedErrors();
                opt.EnableSensitiveDataLogging();
            }
        });
    }

    private static void AddCache(
        IServiceCollection s,
        IConfiguration config
    )
    {
        string host = config["REDIS_HOST"]
        ?? throw new Exception("REDIS_HOST is required");

        string port = config["REDIS_PORT"]
        ?? throw new Exception("REDIS_PORT is required");

        var options = new ConfigurationOptions
        {
            EndPoints =
            {
                $"{host}:{port}"
            },
            DefaultDatabase = 0
        };

        s.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(options));
        s.AddSingleton<ICacheProvider, RedisCacheProvider>();
    }

    private static void AddAuth(
        IServiceCollection s, 
        IConfiguration config
    )
    {
        int acessTokenExpiration = int.Parse(
            config["ASPNETCORE_ENVIRONMENT"] == "Development"
                ? $"{int.MaxValue}"
                : config["JWT_ACCESS_TOKEN_EXPIRATION"]
                ?? throw new Exception("JWT_ACCESS_TOKEN_EXPIRATION is required")
        );

        int refreshTokenExpiration = int.Parse(
            config["JWT_REFRESH_TOKEN_EXPIRATION"]
            ?? throw new Exception("JWT_REFRESH_TOKEN_EXPIRATION is required")
        );

        string issuer = config["JWT_ISSUER"]
        ?? throw new Exception("JWT_ISSUER is required");
        string audience = config["JWT_AUDIENCE"]
        ?? throw new Exception("JWT_AUDIENCE is required");
        string secretKey = config["JWT_SECRET_KEY"]
        ?? throw new Exception("JWT_SECRET_KEY is required");

        s.ConfigureOptions<AuthOptionsSetup>();
        s.AddAuthorization();
        s.AddAuthentication().AddJwtBearer();
        s.Configure<JwtOptions>((opt) =>
        {
            opt.AccessTokenExpiration = acessTokenExpiration;
            opt.RefreshTokenExpiration = refreshTokenExpiration;
            opt.Issuer = issuer;
            opt.Audience = audience;
            opt.SecretKey = secretKey;
        });
    }
}
