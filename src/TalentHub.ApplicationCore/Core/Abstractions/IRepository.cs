using Ardalis.Specification;

namespace TalentHub.ApplicationCore.Core.Abstractions;

public interface IRepository<TAggregate> : IRepositoryBase<TAggregate>
    where TAggregate : AggregateRoot;
