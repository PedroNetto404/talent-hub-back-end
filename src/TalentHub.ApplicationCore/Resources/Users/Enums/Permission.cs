using System.Runtime.CompilerServices;
using Ardalis.SmartEnum;

namespace TalentHub.ApplicationCore.Resources.Users.Enums;

public sealed class Permission : SmartEnum<Permission>
{
    private Permission(string name, int value) : base(name, value) { }

    public static readonly Permission CreateCompany = new("company:create", 1);
    public static readonly Permission ReadAllCompanies = new("company:read_all", 2);
    public static readonly Permission ReadCompanyById = new("company:read_by_id", 3);
    public static readonly Permission ReadSelfOnlyCompany = new("company:read_self_only", 4);
    public static readonly Permission UpdateCompany = new("company:update", 5);
    public static readonly Permission DeleteCompany = new("company:delete", 6);

    public static readonly Permission CreateCandidate = new("candidate:create", 7);
    public static readonly Permission ReadAllCandidates = new("candidate:read_all", 8);
    public static readonly Permission ReadCandidateById = new("candidate:read_by_id", 9);
    public static readonly Permission ReadSelfOnlyCandidate = new("candidate:read_self_only", 10);
    public static readonly Permission UpdateCandidate = new("candidate:update", 11);
    public static readonly Permission DeleteCandidate = new("candidate:delete", 12);

    public static readonly Permission CreateJobOpportunity = new("job_opening:create", 13);
    public static readonly Permission ReadAllJobOpportunities = new("job_opening:read_all", 14);
    public static readonly Permission ReadJobOpportunityById = new("job_opening:read_by_id", 15);
    public static readonly Permission ReadSelfOnlyJobOpportunity = new("job_opening:read_self_only", 16);
    public static readonly Permission UpdateJobOpportunity = new("job_opening:update", 17);
    public static readonly Permission DeleteJobOpportunity = new("job_opening:delete", 18);

    public static readonly Permission CreateSkill = new("skill:create", 19);
    public static readonly Permission ReadAllSkill = new("skill:read_all", 20);
    public static readonly Permission ReadSkillById = new("skill:read_by_id", 21);
    public static readonly Permission UpdateSkill = new("skill:update", 22);
    public static readonly Permission DeleteSkill = new("skill:delete", 23);

    public static readonly Permission CreateSector = new("sector:create", 24);
    public static readonly Permission ReadAllSectors = new("sector:read_all", 25);
    public static readonly Permission ReadSectorById = new("sector:read_by_id", 26);
    public static readonly Permission UpdateSector = new("sector:update", 27);
    public static readonly Permission DeleteSector = new("sector:delete", 28);

    public static readonly Permission CreateUniversity = new("university:create", 29);
    public static readonly Permission ReadAllUniversities = new("university:read_all", 30);
    public static readonly Permission ReadUniversityById = new("university:read_by_id", 31);
    public static readonly Permission UpdateUniversity = new("university:update", 32);
    public static readonly Permission DeleteUniversity = new("university:delete", 33);

    public static readonly Permission CreateCourse = new("course:create", 34);
    public static readonly Permission ReadAllCourses = new("course:read_all", 35);
    public static readonly Permission ReadCourseById = new("course:read_by_id", 36);
    public static readonly Permission UpdateCourse = new("course:update", 37);
    public static readonly Permission DeleteCourse = new("course:delete", 38);

    public static readonly Permission CreateJobApplication = new("job_application:create", 39);
    public static readonly Permission ReadAllJobApplications = new("job_application:read_all", 40);
    public static readonly Permission ReadJobApplicationById = new("job_application:read_by_id", 41);
    public static readonly Permission ReadSelfOnlyJobApplication = new("job_application:read_self_only", 42);
    public static readonly Permission UpdateJobApplication = new("job_application:update", 43);
    public static readonly Permission DeleteJobApplication = new("job_application:delete", 44);

    public static readonly Permission CreateUser = new("user:create", 45);
    public static readonly Permission ReadAllUsers = new("user:read_all", 46);
    public static readonly Permission ReadUserById = new("user:read_by_id", 47);
    public static readonly Permission ReadSelfOnlyUser = new("user:read_self_only", 48);
    public static readonly Permission UpdateUser = new("user:update", 49);
    public static readonly Permission DeleteUser = new("user:delete", 50);

    public static IEnumerable<Permission> FromRole(Role role)
    {
        if (role == Role.Admin)
        {
            return List;
        }

        if (role == Role.Company)
        {
            return new List<Permission>
            {
                CreateJobOpportunity,
                ReadAllJobOpportunities,
                ReadSelfOnlyJobOpportunity,
                UpdateJobOpportunity,
                DeleteJobOpportunity,

                CreateCandidate,
                ReadSelfOnlyCandidate,
                UpdateCandidate,
                DeleteCandidate,

                CreateJobApplication,
                ReadAllJobApplications,
                ReadSelfOnlyJobApplication,
                UpdateJobApplication,
                DeleteJobApplication,

                ReadSelfOnlyCompany,
                UpdateCompany
            };
        }

        if (role == Role.Candidate)
        {
            return new List<Permission>
            {
                ReadAllJobOpportunities,

                ReadSelfOnlyCandidate,
                UpdateCandidate,
                DeleteCandidate,

                CreateJobApplication,
                ReadSelfOnlyJobApplication,
                UpdateJobApplication,
                DeleteJobApplication
            };
        }

        return new List<Permission>();
    }

    public static implicit operator string(Permission permission) => permission.Name;
}
