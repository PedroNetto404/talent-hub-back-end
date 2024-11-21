using System.Text.Json;

namespace TalentHub.ApplicationCore.Extensions;

public static class ObjectExtensions
{
    private static readonly JsonSerializerOptions _options = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
        WriteIndented = true,
    };

    public static string ToJson(this object obj) =>
        JsonSerializer.Serialize(obj, _options);

    public static T FromJson<T>(this string json) =>
        JsonSerializer.Deserialize<T>(json, _options)!;
}

public static class StringExtensions
{
    public static bool IsValidUrl(this string url) =>
        Uri.IsWellFormedUriString(url, UriKind.Absolute);
}