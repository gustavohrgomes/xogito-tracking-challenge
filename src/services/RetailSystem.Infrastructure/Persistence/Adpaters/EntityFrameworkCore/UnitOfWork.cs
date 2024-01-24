using MediatR;
using Microsoft.EntityFrameworkCore;
using Warehouse.Tracking.Domain.Common;
using Warehouse.Tracking.Domain.Repositories;

namespace Warehouse.Tracking.Infrastructure.Persistence.Adpaters.EntityFrameworkCore;

public class UnitOfWork<TDbContext> : IUnitOfWork
    where TDbContext : DbContext
{
    private readonly TDbContext _context;
    private readonly IPublisher _publisher;

    public UnitOfWork(TDbContext context, IPublisher publisher)
    {
        _context = context;
        _publisher = publisher;
    }

    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        var strategy = _context.Database.CreateExecutionStrategy();

        await strategy.ExecuteAsync(async () =>
        {
            try
            {
                await _context.Database.BeginTransactionAsync(cancellationToken);

                await _context.SaveChangesAsync(cancellationToken);

                var domainEvents = ExtractDomainEvents();

                await _context.SaveChangesAsync(cancellationToken);

                await _context.Database.CommitTransactionAsync(cancellationToken);

                var dispatchingTasks = domainEvents.Select(domainEvent => _publisher.Publish(domainEvent, cancellationToken));

                await Task.WhenAll(dispatchingTasks);
            }
            catch
            {
                await _context.Database.RollbackTransactionAsync(cancellationToken);

                throw;
            }
        });
    }

    public IEnumerable<INotification> ExtractDomainEvents()
    {
        var entities = _context.ChangeTracker
            .Entries<Entity>()
            .Where(x => x.Entity.DomainEvents.Any())
            .ToList();

        var domainEvents = entities.SelectMany(x => x.Entity.DomainEvents).ToList();

        foreach (var entityEntry in entities)
        {
            entityEntry.Entity.ClearDomainEvents();
        }

        return domainEvents;
    }
}
