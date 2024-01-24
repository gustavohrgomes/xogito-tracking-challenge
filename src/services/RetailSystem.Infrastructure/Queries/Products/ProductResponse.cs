using RetailSystem.Domain.Products;

namespace RetailSystem.Infrastructure.Queries.Products;

public record ProductResponse(
    Guid ProductId, 
    string Name, 
    string Status, 
    int quantity, 
    Guid WarehouseId, 
    IEnumerable<MovementHistory> Movements);

public record MovementHistory(
    string? Source, 
    string? Destination, 
    string Status, 
    string? Description,
    int MovementQuantity,
    DateTime? InventoryEntryDate,
    DateTime? LastUpdatedDate, 
    DateTime? DispatchDate,
    DateTime? ReceivedDate);
