namespace Warehouse.Tracking.API.Contracts.Requests;

public record ShipProductRequest(
    Guid ProductId,
    int ProductQuantity,
    Guid DestinationId);
