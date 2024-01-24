using MediatR;
using Warehouse.Tracking.Domain.Products;
using Warehouse.Tracking.Domain.Repositories;

namespace Warehouse.Tracking.Application.Products.Register;

public sealed class ProductInventoryEntryEventHandler : INotificationHandler<ProductInventoryEntryDomainEvent>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ProductInventoryEntryEventHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public Task Handle(ProductInventoryEntryDomainEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
