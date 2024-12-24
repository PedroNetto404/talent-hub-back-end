using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TalentHub.Infra.Security.Options;

namespace TalentHub.Infra.Security;

public sealed class AuthOptionsSetup(
    IOptions<JwtOptions> options
) :
    IConfigureNamedOptions<AuthenticationOptions>,
    IConfigureNamedOptions<JwtBearerOptions>,
    IConfigureNamedOptions<AuthorizationOptions>
{
    private readonly JwtOptions opt = options.Value;

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
            ValidIssuer = opt.Issuer,
            ValidateIssuer = true,

            ValidAudience = opt.Audience,
            ValidateAudience = true,

            ValidateLifetime = true,

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(opt.SecretKey)
            )
        };
    }

    public void Configure(string? name, AuthorizationOptions options) =>
        Configure(options);

    public void Configure(AuthorizationOptions options)
    {
    }
}
