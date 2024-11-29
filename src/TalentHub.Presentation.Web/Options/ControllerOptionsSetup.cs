using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TalentHub.Infra.Json.Converters;
using TalentHub.Presentation.Web.Binders;

namespace TalentHub.Presentation.Web.Options;

public sealed class ControllerOptionsSetup : 
    IConfigureNamedOptions<MvcOptions>, 
    IConfigureNamedOptions<JsonOptions>,
    IConfigureNamedOptions<RouteOptions>
{
    public void Configure(string? name, MvcOptions options) => Configure(options);
    public void Configure(string? name, JsonOptions options) => Configure(options);
    public void Configure(string? name, RouteOptions options) => Configure(options);

    public void Configure(MvcOptions options)
    {
        options.ModelBinderProviders.Insert(0, new DateOnlyModelBinderProvider());
    }

    public void Configure(JsonOptions options)
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.Converters.Add(new SnakeCaseEnumConverterFactory());
    }

    public void Configure(RouteOptions options)
    {
        options.LowercaseQueryStrings = true;
        options.LowercaseUrls = true;
    }
}
