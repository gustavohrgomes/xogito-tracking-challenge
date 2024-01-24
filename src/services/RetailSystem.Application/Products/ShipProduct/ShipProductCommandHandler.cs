using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using Warehouse.Tracking.Domain.Products;
using Warehouse.Tracking.Domain.Repositories;
using Warehouse.Tracking.Domain.Warehouses;

namespace Warehouse.Tracking.Application.Products.ShipProduct;

public class ShipProductCommandHandler : IRequestHandler<ShipProductCommand, Result>
{
    private readonly ILogger<ShipProductCommandHandler> _logger;

    private readonly IProductRepository _productRepository;
    private readonly IWarehouseRepository _warehouseRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ShipProductCommandHandler(
        ILogger<ShipProductCommandHandler> logger,
        IProductRepository productRepository,
        IWarehouseRepository warehouseRepository,
        IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _productRepository = productRepository;
        _warehouseRepository = warehouseRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(ShipProductCommand request, CancellationToken cancellationToken)
    {
        Product product = await _productRepository.FindByIdAsync(request.ProductId, cancellationToken)
            .ConfigureAwait(continueOnCapturedContext: false);

        ProductWarehouse warehouse = await _warehouseRepository.FindByIdAsync(request.DestinationId);

        if (product is null)
        {
            _logger.LogError("Product with ID '{ProductId}' was not found", request.ProductId);
            return Result.Fail(new Error("Product not found"));
        }

        if (warehouse is null)
        {
            _logger.LogError("Warehouse destination with ID '{DestinationId}' was not found", request.DestinationId);
            return Result.Fail(new Error("Warehouse Destination not found"));
        }

        product.ShipProduct(request.ProductQuantity, warehouse.Id, warehouse.Name);

        await _unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false);

        return Result.Ok();
    }
}
