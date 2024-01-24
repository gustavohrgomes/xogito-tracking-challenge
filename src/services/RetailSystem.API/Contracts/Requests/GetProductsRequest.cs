namespace Warehouse.Tracking.API.Contracts.Requests;
public record GetProductsRequest(
    int PageSize = 10,
    int PageIndex = 1,
    string SortColumn = "name",
    string SortOrder = "asc",
    string? SearchTerm = null);
