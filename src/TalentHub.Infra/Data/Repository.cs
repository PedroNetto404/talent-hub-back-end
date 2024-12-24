using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Resources.Candidates;

namespace TalentHub.Infra.Data;

public sealed class Repository<T>(TalentHubContext context) :
    RepositoryBase<T>(context),
    IRepository<T>
    where T : AggregateRoot;
