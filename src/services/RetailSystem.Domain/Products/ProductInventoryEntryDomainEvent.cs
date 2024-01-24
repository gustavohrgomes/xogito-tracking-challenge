using RetailSystem.Domain.Common;

namespace RetailSystem.Domain.Products;

public record ProductInventoryEntryDomainEvent(Guid ProductId, Guid WarehouseId) : DomainEvent;