using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Extensions;
using TalentHub.ApplicationCore.Shared.ValueObjects;

namespace TalentHub.ApplicationCore.Resources.Companies;

public sealed class Company : UserAggregateRoot
{
#pragma warning disable CS0628 // New protected member declared in sealed type
    protected Company() { }
#pragma warning restore CS0628 // New protected member declared in sealed type

    private Company(
        string legalName,
        string tradeName,
        string cnpj,
        string? about,
        Guid sectorId,
        Guid userId,
        string recruitmentEmail,
        string? phone,
        bool autoMatchEnabled,
        int employeeCount,
        string? siteUrl,
        Address address,
        string? instagramUrl,
        string? linkedinUrl,
        string? careerPageUrl,
        string? mission,
        string? vision,
        string? values,
        int foundationYear
    )
    {
        LegalName = legalName;
        TradeName = tradeName;
        Cnpj = cnpj;
        About = about;
        SectorId = sectorId;
        UserId = userId;
        RecruitmentEmail = recruitmentEmail;
        Phone = phone;
        AutoMatchEnabled = autoMatchEnabled;
        EmployeeCount = employeeCount;
        SiteUrl = siteUrl;
        Address = address;
        InstagramUrl = instagramUrl;
        LinkedinUrl = linkedinUrl;
        CareerPageUrl = careerPageUrl;
        Mission = mission;
        Vision = vision;
        Values = values;
        FoundationYear = foundationYear;
    }

    public static Result<Company> Create(
        string legalName,
        string tradeName,
        string cnpj,
        string? about,
        Guid sectorId,
        Guid userId,
        string recruitmentEmail,
        string? phone,
        bool autoMatchEnabled,
        int employeeCount,
        string? siteUrl,
        Address address,
        string? instagramUrl,
        string? linkedinUrl,
        string? careerPageUrl,
        string? mission,
        string? vision,
        string? values,
        int foundantionYear
    )
    {
        if (
            Result.FailEarly(
                () => Result.FailIf(string.IsNullOrWhiteSpace(legalName), "invalid legal name"),
                () => Result.FailIf(legalName.Length > MaxLegalNameLength, $"legal name must have up to {MaxLegalNameLength} characters"),
                () => Result.FailIf(string.IsNullOrWhiteSpace(tradeName), "invalid trade name"),
                () => Result.FailIf(tradeName.Length > MaxTradeNameLength, $"trade name must have up to {MaxTradeNameLength} characters"),
                () => Result.FailIf(string.IsNullOrWhiteSpace(cnpj), "invalid cnpj"),
                () => Result.FailIf(string.IsNullOrWhiteSpace(recruitmentEmail), "invalid recruitment email"),
                () => Result.FailIf(employeeCount < 0, "invalid employee count"),
                () => Result.FailIf(address is null, "invalid address")
            ) is
            {
                IsFail: true,
                Error: var err
            }
        )
        {
            return err;
        }

        return Result.Ok(new Company(
            legalName,
            tradeName,
            cnpj,
            about,
            sectorId,
            userId,
            recruitmentEmail,
            phone,
            autoMatchEnabled,
            employeeCount,
            siteUrl,
            address,
            instagramUrl,
            linkedinUrl,
            careerPageUrl,
            mission,
            vision,
            values,
            foundantionYear
        ));
    }

    public string LogoFileName => $"company_logo_{Id}";
    public string PresentationVideoFileName => $"company_presentation_video_{Id}";
    public string GetGaleryItemFileName(string? url = null)
    {
        int index = url is null
        ? _galery.Count
        : _galery.IndexOf(url);

        return $"company_galery_item_{Id}_{index}";
    }

    public const int MaxGaleryItemBytes = 10;
    public const int MaxLogoBytes = 2 * 1024 * 1024;
    public const int MaxPresentationVideoBytes = 50 * 1024 * 1024;
    public const int MaxMissionLength = 500;
    public const int MaxVisionLength = 500;
    public const int MaxValuesLength = 500;
    public const int MaxAboutLength = 500;
    public const int MaxLegalNameLength = 100;
    public const int MaxTradeNameLength = 100;

    private readonly List<string> _galery = [];

    public string LegalName { get; private set; }
    public string TradeName { get; private set; }
    public string Cnpj { get; private set; }
    public string? About { get; private set; }
    public Guid SectorId { get; private set; }
    public string RecruitmentEmail { get; private set; }
    public string? Phone { get; private set; }
    public bool AutoMatchEnabled { get; private set; } = true;
    public int EmployeeCount { get; set; }
    public string? LogoUrl { get; private set; }
    public string? SiteUrl { get; private set; }
    public Address Address { get; private set; }
    public string? InstagramUrl { get; private set; }
    public string? LinkedinUrl { get; private set; }
    public string? CareerPageUrl { get; private set; }
    public string? PresentationVideoUrl { get; private set; }
    public string? Mission { get; private set; }
    public string? Vision { get; private set; }
    public string? Values { get; private set; }
    public int FoundationYear { get; private set; }
    public IReadOnlyList<string> Galery => _galery.AsReadOnly();

    public Result ChangeLegalName(string legalName)
    {
        if (
            Result.FailEarly(
                () => Result.FailIf(string.IsNullOrWhiteSpace(legalName), "invalid legal name"),
                () => Result.FailIf(legalName.Length > MaxLegalNameLength, $"legal name must have up to {MaxLegalNameLength} characters")
            ) is
            {
                IsFail: true,
                Error: var err
            }
        )
        {
            return err;
        }

        LegalName = legalName;

        return Result.Ok();
    }

    public Result ChangeTradeName(string tradeName)
    {
        if (
            Result.FailEarly(
                () => Result.FailIf(string.IsNullOrWhiteSpace(tradeName), "invalid trade name"),
                () => Result.FailIf(tradeName.Length > MaxTradeNameLength, $"trade name must have up to {MaxTradeNameLength} characters")
            ) is
            {
                IsFail: true,
                Error: var err
            }
        )
        {
            return err;
        }

        TradeName = tradeName;

        return Result.Ok();
    }

    public Result ChangeCnpj(string cnpj)
    {
        //TODO:validate
        Cnpj = cnpj;

        return Result.Ok();
    }

    public Result ChangeAbout(string? about)
    {
        if (about is null)
        {
            About = null;
            return Result.Ok();
        }

        if (
            Result.FailEarly(
                () => Result.FailIf(string.IsNullOrWhiteSpace(about), "invalid about"),
                () => Result.FailIf(about.Length > MaxAboutLength, $"about must have up to {MaxAboutLength} characters")
            ) is
            {
                IsFail: true,
                Error: var err
            }
        )
        {
            return err;
        }

        About = about;

        return Result.Ok();
    }

    public Result AddToGalery(string imageUrl)
    {
        if (
            Result.FailEarly(
                () => Result.FailIf(string.IsNullOrWhiteSpace(imageUrl), "invalid image url"),
                () => Result.FailIf(!imageUrl.IsValidUrl(), $"{imageUrl} is not a valid url"),
                () => Result.FailIf(_galery.Count == MaxGaleryItemBytes, "galery supports up to 10 images")
            ) is { IsFail: true, Error: var error }
        )
        {
            return error;
        }

        _galery.Add(imageUrl);

        return Result.Ok();
    }

    public Result RemoveFromGalery(string imageUrl)
    {
        if (
            Result.FailEarly(
                () => Result.FailIf(string.IsNullOrWhiteSpace(imageUrl), "invalid image url"),
                () => Result.FailIf(!imageUrl.IsValidUrl(), $"{imageUrl} is not valid url")
            ) is
            {
                IsFail: true,
                Error: var err
            }
        )
        {
            return err;
        }

        if (!_galery.Remove(imageUrl))
        {
            return Error.InvalidInput("image not found in galery");
        }

        return Result.Ok();
    }

    public Result ChangeSectorId(Guid sectorId)
    {
        if (
            Result.FailIf(sectorId == Guid.Empty, "invalid sector id") is
            {
                IsFail: true,
                Error: var err
            }
        )
        {
            return err;
        }

        SectorId = sectorId;

        return Result.Ok();
    }

    public Result ChangeRecruitmentEmail(string recruitmentEmail)
    {
        //TODO:validate
        RecruitmentEmail = recruitmentEmail;

        return Result.Ok();
    }

    public Result ChangePhone(string? phone)
    {
        if (phone is null)
        {
            Phone = null;
            return Result.Ok();
        }

        //TODO:validate

        Phone = phone;

        return Result.Ok();
    }

    public void SetAutoMatchEnabled(bool autoMatchEnabled) =>
        AutoMatchEnabled = autoMatchEnabled;

    public Result ChangeEmployeeCount(int employeeCount)
    {
        if (
            Result.FailIf(employeeCount < 0, "invalid employee count") is
            {
                IsFail: true,
                Error: var err
            }
        )
        {
            return err;
        }

        EmployeeCount = employeeCount;

        return Result.Ok();
    }

    public Result ChangeLogoUrl(string? logoUrl)
    {
        if (logoUrl is null)
        {
            LogoUrl = null;
            return Result.Ok();
        }

        if (
            Result.FailIf(!logoUrl.IsValidUrl(), $"{logoUrl} is not valid url") is
            {
                IsFail: true,
                Error: var err
            }
        )
        {
            return err;
        }

        LogoUrl = logoUrl;

        return Result.Ok();
    }

    public Result ChangeSiteUrl(string? siteUrl)
    {
        if (siteUrl is null)
        {
            SiteUrl = null;
            return Result.Ok();
        }

        if (
            Result.FailIf(!siteUrl.IsValidUrl(), $"{siteUrl} is not valid url") is
            {
                IsFail: true,
                Error: var err
            }
        )
        {
            return err;
        }

        SiteUrl = siteUrl;

        return Result.Ok();
    }

    public void ChangeAddress(Address address) =>
        Address = address;

    public Result ChangeInstagramUrl(string? instagramUrl)
    {
        if (instagramUrl is null)
        {
            InstagramUrl = null;
            return Result.Ok();
        }

        if (
            Result.FailIf(!instagramUrl.IsValidUrl(), $"{instagramUrl} is not valid url") is
            {
                IsFail: true,
                Error: var err
            }
        )
        {
            return err;
        }

        InstagramUrl = instagramUrl;

        return Result.Ok();
    }

    public Result ChangeLinkedinUrl(string? linkedinUrl)
    {
        if (linkedinUrl is null)
        {
            LinkedinUrl = null;
            return Result.Ok();
        }

        if (
            Result.FailIf(!linkedinUrl.IsValidUrl(), $"{linkedinUrl} is not valid url") is
            {
                IsFail: true,
                Error: var err
            }
        )
        {
            return err;
        }

        LinkedinUrl = linkedinUrl;

        return Result.Ok();
    }

    public Result ChangeCareerPageUrl(string? careerPageUrl)
    {
        if (careerPageUrl is null)
        {
            CareerPageUrl = null;
            return Result.Ok();
        }

        if (
            Result.FailIf(!careerPageUrl.IsValidUrl(), $"{careerPageUrl} is not valid url") is
            {
                IsFail: true,
                Error: var err
            }
        )
        {
            return err;
        }

        CareerPageUrl = careerPageUrl;

        return Result.Ok();
    }

    public Result ChangePresentationVideoUrl(string? presentationVideoUrl)
    {
        if (presentationVideoUrl is null)
        {
            PresentationVideoUrl = null;
            return Result.Ok();
        }

        if (
            Result.FailIf(!presentationVideoUrl.IsValidUrl(), $"{presentationVideoUrl} is not valid url") is
            {
                IsFail: true,
                Error: var err
            }
        )
        {
            return err;
        }

        PresentationVideoUrl = presentationVideoUrl;

        return Result.Ok();
    }

    public Result ChangeMission(string? mission)
    {
        if (mission is null)
        {
            Mission = null;
            return Result.Ok();
        }

        if (
            Result.FailIf(mission.Length > MaxMissionLength, $"mission must have up to {MaxMissionLength} characters") is
            {
                IsFail: true,
                Error: var err
            }
        )
        {
            return err;
        }

        Mission = mission;

        return Result.Ok();
    }

    public Result ChangeVision(string? vision)
    {
        if (vision is null)
        {
            Vision = null;
            return Result.Ok();
        }

        if (
            Result.FailIf(vision.Length > MaxVisionLength, $"vision must have up to {MaxVisionLength} characters") is
            {
                IsFail: true,
                Error: var err
            }
        )
        {
            return err;
        }

        Vision = vision;

        return Result.Ok();
    }

    public Result ChangeValues(string? values)
    {
        if (values is null)
        {
            Values = null;
            return Result.Ok();
        }

        if (
            Result.FailIf(values.Length > MaxValuesLength, $"values must have up to {MaxValuesLength} characters") is
            {
                IsFail: true,
                Error: var err
            }
        )
        {
            return err;
        }

        Values = values;

        return Result.Ok();
    }

    public Result ChangeFoundantionYear(int foundantionYear)
    {
        if (
            Result.FailIf(foundantionYear < 0, "invalid foundation year") is
            {
                IsFail: true,
                Error: var err
            }
        )
        {
            return err;
        }

        FoundationYear = foundantionYear;

        return Result.Ok();
    }
}
