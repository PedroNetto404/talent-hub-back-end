using Ardalis.Specification.EntityFrameworkCore;
using TalentHub.ApplicationCore.Core.Abstractions;

namespace TalentHub.Infra.Data;

public sealed class Repository<T>(TalentHubContext context) :
    RepositoryBase<T>(context),
    IRepository<T>
    where T : AggregateRoot;
