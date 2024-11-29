using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;

namespace TalentHub.ApplicationCore.Candidates.Entities;

public sealed class Certificate : Entity
{
#pragma warning disable CS0628 // New protected member declared in sealed type
    protected Certificate() { }
#pragma warning restore CS0628 // New protected member declared in sealed type


    private Certificate(
        string name,
        string institution,
        double workload,
        string? url)
    {
        Name = name;
        Institution = institution;
        Workload = workload;
        Url = url;
    }

    public static Result<Certificate> Create(string name, string institution, double workload, string? url)
    {
        if (string.IsNullOrWhiteSpace(name))
        { return new Error("certificate", "name is required"); }

        if (string.IsNullOrWhiteSpace(institution))
        { return new Error("certificate", "institution is required"); }

        if (workload <= 0)
        { return new Error("certificate", "invalid workload"); }

        if (string.IsNullOrWhiteSpace(url) || Uri.IsWellFormedUriString(url, UriKind.Absolute))
        { return new Error("certificate", "url is invalid"); }

        return new Certificate(
            name,
            institution,
            workload,
            url);
    }

    public string Name { get; private set; }
    public string Institution { get; private set; }
    public double Workload { get; private set; }
    public string? Url { get; private set; }

    public Result SetUrl(string url)
    {
        if (string.IsNullOrWhiteSpace(url))
        { return new Error("certificate", "invalid certificate url"); }

        Url = url;

        return Result.Ok();
    }
}
