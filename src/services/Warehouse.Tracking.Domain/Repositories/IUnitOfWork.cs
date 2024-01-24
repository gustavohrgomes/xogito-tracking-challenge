using MediatR;

namespace Warehouse.Tracking.Domain.Repositories;
public interface IUnitOfWork
{
    Task CommitAsync(CancellationToken cancellationToken);
    IEnumerable<INotification> ExtractDomainEvents();
}
