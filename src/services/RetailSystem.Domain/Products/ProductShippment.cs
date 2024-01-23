namespace RetailSystem.Domain.Products;

public record ProductShippment(
    Guid ProductId,
    int ProductQuantity,
    Guid SourceId,
    string Source,
    Guid DestinationId,
    string Destination);
