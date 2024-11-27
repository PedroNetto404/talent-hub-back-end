namespace TalentHub.ApplicationCore.Extensions;

public static class StringExtensions
{
    public static bool IsValidUrl(this string url) =>
        Uri.IsWellFormedUriString(url, UriKind.Absolute);
}