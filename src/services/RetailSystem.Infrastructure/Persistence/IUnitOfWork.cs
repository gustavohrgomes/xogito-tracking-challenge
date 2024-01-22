using MediatR;

namespace RetailSystem.Infrastructure.Persistence;

public interface IUnitOfWork
{
    Task CommitAsync();
    IEnumerable<INotification> ExtractDomainEventsFromAggregates();
}
