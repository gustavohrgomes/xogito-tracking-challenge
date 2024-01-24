using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Warehouse.Tracking.API.Contracts.Requests;
using Warehouse.Tracking.Application.Products.Register;

namespace Warehouse.Tracking.API.Endpoints.Products;

[Route("api/products")]
public class RegisterProductEndpoint : EndpointBaseAsync
    .WithRequest<RegisterProductRequest>
    .WithResult<IActionResult>
{
    private readonly ISender _sender;

    public RegisterProductEndpoint(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("register", Name = "RegisterProduct")]
    [SwaggerOperation(
        Summary = "Add new product to warehouse",
        Description = "Add new product to warehouse",
        OperationId = "Add_Product",
        Tags = new[] { "Products" })]
    public override async Task<IActionResult> HandleAsync(RegisterProductRequest request, CancellationToken cancellationToken = default)
    {
        RegisterProductCommand command = new(request.Name, request.Quantity, request.WarehouseId, request.StoreId ?? null);

        var result = await _sender.Send(command, cancellationToken);

        return CreatedAtRoute("RegisterProduct", result.Value);
    }
}