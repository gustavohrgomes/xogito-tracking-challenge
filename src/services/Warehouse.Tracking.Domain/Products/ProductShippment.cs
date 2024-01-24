namespace Warehouse.Tracking.Domain.Products;

public record ProductShippment(
    Guid ProductId,
    int ProductQuantity,
    Guid SourceId,
    string Source,
    Guid DestinationId,
    string Destination);
