using FluentResults;
using MediatR;
using Shared;

namespace RetailSystem.Infrastructure.Queries.Products;

public record GetProductsQuery(
    int PageSize = 10, 
    int PageIndex = 1, 
    string? SortColumn = "name", 
    string? SortOrder = "asc", 
    string? SearchTerm = null) : IRequest<Result<PaginatedResult<ProductResponse>>>;
