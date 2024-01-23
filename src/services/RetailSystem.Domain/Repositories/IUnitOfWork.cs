using MediatR;

namespace RetailSystem.Domain.Repositories;
public interface IUnitOfWork
{
    Task CommitAsync(CancellationToken cancellationToken);
    IEnumerable<INotification> ExtractDomainEvents();
}
