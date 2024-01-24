using Warehouse.Tracking.Domain.Common;

namespace Warehouse.Tracking.Domain.Products;

public record ProductInventoryEntryDomainEvent(Guid ProductId, Guid WarehouseId) : DomainEvent;