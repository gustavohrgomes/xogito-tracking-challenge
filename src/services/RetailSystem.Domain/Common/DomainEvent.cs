using MediatR;

namespace RetailSystem.Domain.Common;

public abstract record DomainEvent : INotification
{
}
