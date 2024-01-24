using FluentResults;
using MediatR;
using Warehouse.Tracking.Shared;

namespace Warehouse.Tracking.Infrastructure.Queries.Products;

public record GetProductsQuery(
    int PageSize = 10,
    int PageIndex = 1,
    string? SortColumn = "name",
    string? SortOrder = "asc",
    string? SearchTerm = null) : IRequest<Result<PaginatedResult<ProductResponse>>>;
