using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using RetailSystem.Domain.Products;
using RetailSystem.Domain.Repositories;

namespace RetailSystem.Application.Products.ShipProduct;

public class ShipProductCommandHandler : IRequestHandler<ShipProductCommand, Result>
{
    private readonly ILogger<ShipProductCommandHandler> _logger;

    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ShipProductCommandHandler(
        ILogger<ShipProductCommandHandler> logger,
        IProductRepository productRepository,
        IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(ShipProductCommand request, CancellationToken cancellationToken)
    {
        Product product = await _productRepository.FindByIdAsync(request.ProductId, cancellationToken)
            .ConfigureAwait(continueOnCapturedContext: false);

        if (product is null)
        {
            _logger.LogError("Product with ID '{ProductId}' not found", request.ProductId);
            return Result.Fail(new Error("Product not found."));
        }

        product.ShipProduct(
            request.ProductQuantity,
            request.DestinationId,
            request.Destination);

        await _unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false);

        return Result.Ok();
    }
}
