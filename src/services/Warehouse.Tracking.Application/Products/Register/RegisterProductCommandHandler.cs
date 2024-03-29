﻿using FluentResults;
using MediatR;
using Warehouse.Tracking.Domain.Products;
using Warehouse.Tracking.Domain.Repositories;

namespace Warehouse.Tracking.Application.Products.Register;
public sealed class RegisterProductCommandHandler : IRequestHandler<RegisterProductCommand, Result<Guid>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProductRepository _productRepository;

    public RegisterProductCommandHandler(IUnitOfWork unitOfWork, IProductRepository productRepository)
    {
        _unitOfWork = unitOfWork;
        _productRepository = productRepository;
    }

    public async Task<Result<Guid>> Handle(RegisterProductCommand request, CancellationToken cancellationToken)
    {
        Product newProduct = new(Guid.NewGuid(), request.Name, request.Quantity, request.WarehouseId);

        newProduct.InventoryEntry();

        _productRepository.Insert(newProduct);

        await _unitOfWork.CommitAsync(cancellationToken);

        return newProduct.Id;
    }
}
