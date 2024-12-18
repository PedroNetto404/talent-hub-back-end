using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Extensions;

namespace TalentHub.ApplicationCore.Resources.Universities;

public sealed class University : AggregateRoot
{
#pragma warning disable CS0628 // New protected member declared in sealed type
#pragma warning disable CS8618, CS9264
    protected University() { }
#pragma warning restore CS8618, CS9264
#pragma warning restore CS0628 // New protected member declared in sealed type

    private University(string name, string? siteUrl = null) =>
        (Name, SiteUrl) = (name, siteUrl);

    public static Result<University> Create(string name, string? siteUrl = null)
    {
        if (Result.FailIf(string.IsNullOrWhiteSpace(name), "Name must be provided.") is { IsFail: true, Error: var nameError })
        {
            return nameError;
        }

        if (siteUrl is not null && !siteUrl.IsValidUrl())
        {
            return new Error("university", "Invalid site url.");
        }

        return Result.Ok(new University(name, siteUrl));
    }

    public string Name { get; private set; }
    public string? SiteUrl { get; private set; }

    public Result ChangeName(string name)
    {
        if (Result.FailIf(string.IsNullOrWhiteSpace(name), "Name must be provided.") is { IsFail: true, Error: var nameError })
        {
            return nameError;
        }

        Name = name;
        return Result.Ok();
    }

    public Result ChangeSiteUrl(string siteUrl)
    {
        if (Result.FailIf(!siteUrl.IsValidUrl(), "Invalid site url.") is { IsFail: true, Error: var siteUrlError })
        {
            return siteUrlError;
        }

        SiteUrl = siteUrl;
        return Result.Ok();
    }
}
