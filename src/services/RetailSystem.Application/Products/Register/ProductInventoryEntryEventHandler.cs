using MediatR;
using RetailSystem.Domain.Products;
using RetailSystem.Domain.Repositories;

namespace RetailSystem.Application.Products.Register;

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
