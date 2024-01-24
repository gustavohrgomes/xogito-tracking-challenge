namespace RetailSystem.API.Contracts.Requests;

public record RegisterProductRequest(string Name, int Quantity, Guid WarehouseId, Guid? StoreId);
