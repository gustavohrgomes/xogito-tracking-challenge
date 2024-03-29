﻿using Warehouse.Tracking.Domain.Products;
using Warehouse.Tracking.Infrastructure.Queries.Products;

namespace Warehouse.Tracking.Infrastructure.Extensions;
public static partial class Extensions
{
    public static ProductResponse ToProductResponse(this Product product)
    {
        return new(
            product.Id,
            product.Name,
            product.State.ToString(),
            product.Quantity,
            product.WarehouseId,
            product.ProductMovements.Select(movement => new MovementHistory(
                                            movement.Source,
                                            movement.Destination,
                                            movement.State.ToString(),
                                            movement.Description,
                                            movement.ProductQuantity,
                                            movement.InventoryEntryUtcDate,
                                            movement.LastUpdatedUtcDate,
                                            movement.DispatchUtcDate,
                                            movement.ReceivedUtcDate)).ToList());



    }
}
