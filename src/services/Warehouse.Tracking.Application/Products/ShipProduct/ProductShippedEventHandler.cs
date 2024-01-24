using MediatR;
using Warehouse.Tracking.Domain.Products;
using Warehouse.Tracking.Domain.Repositories;

namespace Warehouse.Tracking.Application.Products.ShipProduct;

public class ProductShippedEventHandler : INotificationHandler<ProductShippedDomainEvent>
{
    private const int ProductQuantityThreshold = 10;

    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ProductShippedEventHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(ProductShippedDomainEvent notification, CancellationToken cancellationToken)
    {
        var product = await _productRepository.FindByIdAsync(notification.ProductId, cancellationToken);

        if (product.Quantity < ProductQuantityThreshold)
        {
            product.LowStock();
        }

        if (product.Quantity == 0)
        {
            product.OutOfStock();
        }

        await _unitOfWork.CommitAsync(cancellationToken);
    }
}
