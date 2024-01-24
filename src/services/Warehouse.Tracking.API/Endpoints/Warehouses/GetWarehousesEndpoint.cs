using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using Warehouse.Tracking.Infrastructure.Persistence;

namespace Warehouse.Tracking.API.Endpoints.Warehouses;

[Route("api/warehouse")]
public class GetWarehousesEndpoint : EndpointBaseAsync
    .WithoutRequest
    .WithActionResult
{
    private readonly ApplicationDbContext _context;

    public GetWarehousesEndpoint(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get warehouses",
        Description = "Get Get warehouses",
        OperationId = "Get_Warehouse",
        Tags = new[] { "Warehouses" })]
    public override async Task<ActionResult> HandleAsync(CancellationToken cancellationToken = default)
    {
        var warehouses = await _context.Warehouses.ToListAsync(cancellationToken);

        var warehousesResponse = warehouses.Select(x => new WarehousesResponse(x.Id, x.Name));

        return Ok(warehousesResponse);
    }
}

public record WarehousesResponse(Guid Id, string Name);