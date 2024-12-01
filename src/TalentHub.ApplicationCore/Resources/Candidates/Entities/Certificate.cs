using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Extensions;

namespace TalentHub.ApplicationCore.Resources.Candidates.Entities;

public sealed class Certificate : Entity
{
#pragma warning disable CS0628 // New protected member declared in sealed type
    protected Certificate() { }
#pragma warning restore CS0628 // New protected member declared in sealed type

    private Certificate(
        string name,
        string issuer,
        double workload,
        string? url)
    {
        Name = name;
        Issuer = issuer;
        Workload = workload;
        Url = url;
    }

    public static Result<Certificate> Create(string name, string issuer, double workload, string? url)
    {
        if (string.IsNullOrWhiteSpace(name))
        { return new Error("certificate", "name is required"); }

        if (string.IsNullOrWhiteSpace(issuer))
        { return new Error("certificate", "institution is required"); }

        if (workload <= 0)
        { return new Error("certificate", "invalid workload"); }

        if (string.IsNullOrWhiteSpace(url) || Uri.IsWellFormedUriString(url, UriKind.Absolute))
        { return new Error("certificate", "url is invalid"); }

        return new Certificate(
            name,
            issuer,
            workload,
            url);
    }

    private readonly List<Guid> _relatedSkills = [];
    
    public string Name { get; private set; }
    public string Issuer { get; private set; }
    public double Workload { get; private set; }
    public string? Url { get; private set; }
    public IReadOnlyList<Guid> RelatedSkills => _relatedSkills.AsReadOnly();

    public Result SetUrl(string url)
    {
        if (string.IsNullOrWhiteSpace(url))
        { return new Error("certificate", "invalid certificate url"); }

        Url = url;

        return Result.Ok();
    }

    public void ClearRelatedSkills() => _relatedSkills.Clear();

    public Result AddRelatedSkill(Guid relatedSkillId)
    {
        if (_relatedSkills.Contains(relatedSkillId))
        {
            return new Error("candidate_certificate", $"skill {relatedSkillId} already added"); 
        }
        
        _relatedSkills.Add(relatedSkillId);
        return Result.Ok();
    }

    public Result ChangeName(string name)
    {
        if(string.IsNullOrWhiteSpace(name))
        {
            return Error.BadRequest("certificate name is required");
        }

        Name = name;
        
        return Result.Ok();
    }

    public Result ChangeIssuer(string name)
    {
        throw new NotImplementedException();
    }

    public Result ChangeWorkload(double workload)
    {
        if(workload < 0)
        {
            return Error.BadRequest("workload must be greater than 0");
        }

        Workload = workload;

        return Result.Ok();
    }

    public Result ChangeUrl(string? url)
    {
        if(url is null)
        {
            Url = null;
            return Result.Ok();
        }

        if(!url.IsValidUrl())
        {
            return Error.BadRequest($"{url} is not valid url");
        }

        Url = url;

        return Result.Ok();
    }
}
