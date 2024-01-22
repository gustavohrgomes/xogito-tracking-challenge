using MediatR;
using Microsoft.EntityFrameworkCore;
using RetailSystem.Domain.Common;

namespace RetailSystem.Infrastructure.Persistence.Adpaters.EntityFrameworkCore;

public class UnitOfWork<TDbContext> : IUnitOfWork
    where TDbContext : DbContext
{
    private readonly TDbContext _context;

    public UnitOfWork(TDbContext context)
    {
        _context = context;
    }

    public async Task CommitAsync() => await _context.SaveChangesAsync();

    public IEnumerable<INotification> ExtractDomainEventsFromAggregates()
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

    Task IUnitOfWork.CommitAsync()
    {
        throw new NotImplementedException();
    }
}
