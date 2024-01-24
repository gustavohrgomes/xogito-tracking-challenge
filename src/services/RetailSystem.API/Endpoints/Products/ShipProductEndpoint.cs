using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RetailSystem.API.Contracts.Requests;
using RetailSystem.Application.Products.ShipProduct;
using Swashbuckle.AspNetCore.Annotations;

namespace RetailSystem.API.Endpoints.Products;

[Route("api/products")]
public class ShipProductEndpoint : EndpointBaseAsync
    .WithRequest<ShipProductRequest>
    .WithResult<IActionResult>
{
    private readonly ISender _sender;

    public ShipProductEndpoint(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("ship-to-store")]
    [SwaggerOperation(
        Summary = "Ship products to the stores",
        Description = "Ship products to the stores",
        OperationId = "Ship_Products",
        Tags = new[] { "Products" })]
    public override async Task<IActionResult> HandleAsync(ShipProductRequest request, CancellationToken cancellationToken = default)
    {
        ShipProductCommand command = new(
            request.ProductId, 
            request.ProductQuantity, 
            request.DestinationId,
            request.Destination);

        var result = await _sender.Send(command, cancellationToken);

        return result.IsSuccess 
            ? Accepted()
            : BadRequest(result.Errors);
    }
}
