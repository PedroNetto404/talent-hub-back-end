using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Extensions;

namespace TalentHub.ApplicationCore.Resources.Candidates.SubResources.Certificates;

public sealed class Certificate : Entity
{
#pragma warning disable CS0628 // New protected member declared in sealed type
    protected Certificate() { }
#pragma warning restore CS0628 // New protected member declared in sealed type

    private Certificate(
        string name,
        string issuer,
        double workload)
    {
        Name = name;
        Issuer = issuer;
        Workload = workload;
    }

    public static Result<Certificate> Create(string name, string issuer, double workload)
    {
        if (string.IsNullOrWhiteSpace(name))
        { return new Error("certificate", "name is required"); }

        if (string.IsNullOrWhiteSpace(issuer))
        { return new Error("certificate", "institution is required"); }

        if (workload <= 0)
        { return new Error("certificate", "invalid workload"); }

        return new Certificate(
            name,
            issuer,
            workload);
    }

    public string AttachmentFileName => $"candidate-certificate-attachment-{Id}";
    
    private readonly List<Guid> _relatedSkills = [];
    public string Name { get; private set; }
    public string Issuer { get; private set; }
    public double Workload { get; private set; }
    public string? AttachmentUrl { get; private set; }
    public IReadOnlyList<Guid> RelatedSkills => _relatedSkills.AsReadOnly();

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
        if (string.IsNullOrWhiteSpace(name))
        {
            return Error.BadRequest("invalid certificate name");
        }

        Name = name;
        
        return Result.Ok();
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

    public Result ChangeAttachmentUrl(string? url)
    {
        if(url is null)
        {
            AttachmentUrl = null;
            return Result.Ok();
        }

        if(!url.IsValidUrl())
        {
            return Error.BadRequest($"{url} is not valid url");
        }

        AttachmentUrl = url;

        return Result.Ok();
    }
}
