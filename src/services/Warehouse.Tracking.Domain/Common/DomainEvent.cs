using MediatR;

namespace Warehouse.Tracking.Domain.Common;

public abstract record DomainEvent : INotification
{ }
