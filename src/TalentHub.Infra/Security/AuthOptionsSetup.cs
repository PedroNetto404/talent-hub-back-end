using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace TalentHub.Infra.Security;

public sealed class AuthOptionsSetup(
    IConfiguration configuration
) :
    IConfigureNamedOptions<AuthenticationOptions>,
    IConfigureNamedOptions<JwtBearerOptions>,
    IConfigureNamedOptions<AuthorizationOptions>
{
    public void Configure(string? name, AuthenticationOptions options) => 
        Configure(options);

    public void Configure(AuthenticationOptions options)
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }

    public void Configure(string? name, JwtBearerOptions options) =>
        Configure(options);

    public void Configure(JwtBearerOptions options)
    {
        options.TokenValidationParameters = new()
        {
            ValidIssuer = configuration["Jwt:Issuer"],
            ValidateIssuer = true,

            ValidAudience = configuration["Jwt:Audience"],
            ValidateAudience = true,

            ValidateLifetime = true,

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    configuration["Jwt:SecretKey"]!
                )
            )
        };
    }

    public void Configure(string? name, AuthorizationOptions options) =>
        Configure(options);

    public void Configure(AuthorizationOptions options)
    {
    }
}
