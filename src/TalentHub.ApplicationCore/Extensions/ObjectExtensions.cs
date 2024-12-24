using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace TalentHub.ApplicationCore.Extensions;

public static class ObjectExtensions
{
    private static readonly JsonSerializerOptions _options = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    };

    public static string ToJson(this object obj) =>
        JsonSerializer.Serialize(obj, _options);

    public static T FromJson<T>(this string json) =>
        JsonSerializer.Deserialize<T>(json, _options)!;
}
