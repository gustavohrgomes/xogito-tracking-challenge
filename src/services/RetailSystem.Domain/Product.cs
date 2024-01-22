using RetailSystem.Domain.Common;

namespace RetailSystem.Domain;

public class Product : Entity
{
    public Product(Guid id, string name, int quantity, Guid warehouseId)
    {
        Id = id;
        Name = name;
        Quantity = quantity;
        WarehouseId = warehouseId;
        State = ProductState.InStock;
    }

    protected Product() { }

    public string Name { get; private set; }
    public int Quantity { get; private set; }
    public ProductState State { get; private set; }
    public Guid WarehouseId { get; private set; }
    public Guid? StoreId { get; private set; }
}
