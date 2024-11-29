using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Extensions;

namespace TalentHub.ApplicationCore.Universities;

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
        if(string.IsNullOrWhiteSpace(name)) return new Error("university", "Name must be provided.");
        if(siteUrl is not null && !siteUrl.IsValidUrl()) return new Error("university", "Invalid site url.");
        
        return Result.Ok(new University(name, siteUrl));
    }
    public string Name { get; private set; }
    public string? SiteUrl { get; private set; }

    public Result ChangeName(string name)
    {
        if(string.IsNullOrWhiteSpace(name)) return new Error("university", "Name must be provided.");
        Name = name;
        return Result.Ok();
    }

    public Result ChangeSiteUrl(string siteUrl)
    {
        if(!siteUrl.IsValidUrl()) return new Error("university", "Invalid site url.");
        SiteUrl = siteUrl;
        return Result.Ok();
    }
}