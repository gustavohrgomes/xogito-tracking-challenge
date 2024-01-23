using RetailSystem.Domain.Common;
using RetailSystem.Domain.Exceptions;

namespace RetailSystem.Domain.Products;

public class Product : Entity
{
    private readonly List<ProductMovement> _productMovements = new();

    public Product(Guid id, string name, int quantity, Guid warehouseId)
    {
        Id = id;
        Name = name;
        Quantity = quantity;
        WarehouseId = warehouseId;
        RegisteredUtcDate = DateTime.UtcNow;
        State = ProductState.InStock;
    }

    protected Product() { }

    public string Name { get; set; }
    public int Quantity { get; private set; }
    public DateTime RegisteredUtcDate { get; private set; }
    public ProductState State { get; private set; }
    public Guid WarehouseId { get; private set; }
    public Guid? StoreId { get; private set; }

    public IEnumerable<ProductMovement> ProductMovements => _productMovements.AsReadOnly();

    /// <summary>
    /// Testing Purposes
    /// </summary>
    /// <param name="name"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public void UpdateName(string name)
    {
        if (string.IsNullOrEmpty(name)) throw new ArgumentNullException("Name argument can not be null.");

        Name = name;

        _productMovements.Add(ProductMovement.Create(10, WarehouseId, "Main Warehouse", Guid.NewGuid(), "Some store Warehouse"));
    }

    public void ShipProduct(
        int productQuantity,
        Guid destinationId,
        string destination)
    {
        if (productQuantity > Quantity) throw new InvalidProductQuantityException("Quantity to ship should be equal or lesser than quantity in stock.");
        if (productQuantity <= 0) throw new InvalidProductQuantityException("Product quantity should be greater than zero.");

        Quantity -= productQuantity;

        var movement = ProductMovement.Create(productQuantity, WarehouseId, "Main Warehouse", destinationId, destination);

        _productMovements.Add(movement);

        RaiseDomainEvent(new ProductShippedDomainEvent(Id));
    }

    public void InventoryEntry()
    {
        var movement = ProductMovement.EntryMovement(Quantity);

        _productMovements.Add(movement);

        RaiseDomainEvent(new ProductInventoryEntryDomainEvent(Id, WarehouseId));
    }

    public void InStock() => State = ProductState.InStock;

    public void LowStock() => State = ProductState.LowStock;

    public void OutOfStock() => State = ProductState.OutOfStock;
}
