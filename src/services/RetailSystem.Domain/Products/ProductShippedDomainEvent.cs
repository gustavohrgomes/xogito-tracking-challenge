using RetailSystem.Domain.Common;

namespace RetailSystem.Domain.Products;

public record ProductShippedDomainEvent(Guid ProductId) : DomainEvent;