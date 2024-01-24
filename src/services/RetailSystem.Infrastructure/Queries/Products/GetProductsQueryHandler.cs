using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;
using System.Linq.Expressions;
using Warehouse.Tracking.Domain.Products;
using Warehouse.Tracking.Infrastructure.Extensions;
using Warehouse.Tracking.Infrastructure.Persistence;
using Warehouse.Tracking.Shared;

namespace Warehouse.Tracking.Infrastructure.Queries.Products;

public sealed class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, Result<PaginatedResult<ProductResponse>>>
{
    private readonly ApplicationDbContext _context;

    public GetProductsQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<PaginatedResult<ProductResponse>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Product> productsQuery = _context.Products;

        int totalRecords = productsQuery.Count();

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            productsQuery = productsQuery.Where(x => x.Name.Contains(request.SearchTerm));
        }

        if (request.SortOrder?.ToLower() == "desc")
        {
            productsQuery = productsQuery.OrderByDescending(GetSortProperty(request));
        }
        else
        {
            productsQuery = productsQuery.OrderBy(GetSortProperty(request));
        }

        var products = await productsQuery
            .Skip((request.PageIndex - 1) * request.PageSize)
            .Take(request.PageSize)
            .Include(x => EF.Property<ProductMovement>(x, "_productMovements"))
            .ToListAsync(cancellationToken);

        var productsResponse = products
            .Select(x => x.ToProductResponse())
            .ToImmutableList();

        return new PaginatedResult<ProductResponse>
        {
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
            TotalRecords = totalRecords,
            Query = request.SearchTerm,
            Records = productsResponse,
        };
    }

    private static Expression<Func<Product, object>> GetSortProperty(GetProductsQuery request)
    {
        return request.SortColumn?.ToLower() switch
        {
            "name" => product => product.Name,
            "quantity" => product => product.Quantity,
            _ => product => product.RegisteredUtcDate
        };
    }
}
