namespace Warehouse.Tracking.Domain.Warehouses;

public interface IWarehouseRepository
{
    Task<ProductWarehouse> FindByIdAsync(Guid id);
}
