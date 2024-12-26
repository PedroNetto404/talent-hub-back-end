using Ardalis.SmartEnum;

namespace TalentHub.ApplicationCore.Resources.Users.Enums;

public sealed class Permission : SmartEnum<Permission>
{
    private Permission(string name, int value) : base(name, value)
    {
    }

    public static readonly Permission CreateCompany = new("company:create", 1);
    public static readonly Permission ReadCompany = new("company:read", 2);
    public static readonly Permission UpdateCompany = new("company:update", 3);
    public static readonly Permission DeleteCompany = new("company:delete", 4);

    public static readonly Permission CreateCandidate = new("candidate:create", 5);
    public static readonly Permission ReadCandidate = new("candidate:read", 6);
    public static readonly Permission UpdateCandidate = new("candidate:update", 7);
    public static readonly Permission DeleteCandidate = new("candidate:delete", 8);

    public static readonly Permission CreateJobOpening = new("job_opening:create", 9);
    public static readonly Permission ReadJobOpening = new("job_opening:read", 10);
    public static readonly Permission UpdateJobOpening = new("job_opening:update", 11);
    public static readonly Permission DeleteJobOpening = new("job_opening:delete", 12);

    public static readonly Permission SuggestSkill = new("skill:suggest", 13);
    public static readonly Permission CreateSkill = new("skill:create", 13);
    public static readonly Permission ReadSkill = new("skill:read", 14);
    public static readonly Permission UpdateSkill = new("skill:update", 15);
    public static readonly Permission DeleteSkill = new("skill:delete", 16);

    public static readonly Permission SuggestSector = new("sector:suggest", 17);
    public static readonly Permission CreateSector = new("sector:create", 17);
    public static readonly Permission ReadSector = new("sector:read", 18);
    public static readonly Permission UpdateSector = new("sector:update", 19);
    public static readonly Permission DeleteSector = new("sector:delete", 20);

    public static readonly Permission SuggestUniversity = new("university:suggest", 21);
    public static readonly Permission CreateUniversity = new("university:create", 25);
    public static readonly Permission ReadUniversity = new("university:read", 26);
    public static readonly Permission UpdateUniversity = new("university:update", 27);
    public static readonly Permission DeleteUniversity = new("university:delete", 28);

    public static readonly Permission SuggestCourse = new("course:suggest", 29);
    public static readonly Permission CreateCourse = new("course:create", 29);
    public static readonly Permission ReadCourse = new("course:read", 30);
    public static readonly Permission UpdateCourse = new("course:update", 31);
    public static readonly Permission DeleteCourse = new("course:delete", 32);

    public static readonly Permission CreateJobApplication = new("job_application:create", 21);
    public static readonly Permission ReadJobApplication = new("job_application:read", 22);
    public static readonly Permission UpdateJobApplication = new("job_application:update", 23);
    public static readonly Permission DeleteJobApplication = new("job_application:delete", 24);

    public static IEnumerable<Permission> FromRole(Role role) =>
        role switch
        {
            Role.Admin => List,
            Role.Company =>
            [
                CreateJobOpening,
                ReadJobOpening,
                UpdateJobOpening,
                DeleteJobOpening,

                SuggestSkill,
                ReadSkill,

                SuggestSector,                
                ReadSector,

                CreateJobApplication,
                ReadJobApplication,
                UpdateJobApplication,
                DeleteJobApplication,
                
                SuggestUniversity,
                ReadUniversity,
                
                SuggestSector,
                SuggestCourse
            ],
            Role.Candidate =>
            [
                ReadJobOpening,

                ReadSector,

                ReadJobApplication,
                CreateJobApplication,
                ReadJobApplication,
                UpdateJobApplication,
                DeleteJobApplication,

                SuggestUniversity,
                ReadUniversity,

                SuggestSkill,
                ReadSkill,

                SuggestCourse,
                ReadCourse
            ],
            _ => Enumerable.Empty<Permission>()
        };
}
