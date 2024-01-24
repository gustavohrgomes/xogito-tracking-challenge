using Warehouse.Tracking.Domain.Common;

namespace Warehouse.Tracking.Domain.Products;

public record ProductShippedDomainEvent(Guid ProductId) : DomainEvent;