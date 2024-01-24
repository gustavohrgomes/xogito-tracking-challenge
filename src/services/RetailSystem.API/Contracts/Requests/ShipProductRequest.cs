namespace RetailSystem.API.Contracts.Requests;

public record ShipProductRequest(
    Guid ProductId,
    int ProductQuantity,
    Guid DestinationId,
    string Destination);
