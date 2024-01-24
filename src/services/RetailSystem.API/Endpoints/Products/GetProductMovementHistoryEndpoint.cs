using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Warehouse.Tracking.API.Contracts.Requests;
using Warehouse.Tracking.Infrastructure.Queries.Products;
using Warehouse.Tracking.Shared;

namespace Warehouse.Tracking.API.Endpoints.Products;

[Route("api/products")]
public class GetProductMovementHistoryEndpoint : EndpointBaseAsync
    .WithRequest<GetProductsRequest>
    .WithActionResult<PaginatedResult<ProductResponse>>
{
    private readonly ISender _sender;

    public GetProductMovementHistoryEndpoint(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet(Name = "GetProductsWithHistory")]
    [SwaggerOperation(
        Summary = "Get paginated list of products",
        Description = "Get paginated list of products",
        OperationId = "Get_Products",
        Tags = new[] { "Products" })]
    public override async Task<ActionResult<PaginatedResult<ProductResponse>>> HandleAsync([FromQuery] GetProductsRequest request, CancellationToken cancellationToken = default)
    {
        GetProductsQuery query = new();

        var result = await _sender.Send(query);

        if (result.IsFailed)
        {
            return BadRequest(result.Errors);
        }

        return Ok(result.Value);
    }
}