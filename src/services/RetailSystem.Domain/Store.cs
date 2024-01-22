using RetailSystem.Domain.Common;

namespace RetailSystem.Domain;
public class Store : Entity
{
    private readonly List<Product> _products = new();

    public Store(Guid id, string name, string location, Guid warehouseId)
    {
        Id = id;
        Name = name;
        Location = location;
        WarehouseId = warehouseId;
    }

    public string Name { get; set; }
    public string Location { get; set; }
    public Guid WarehouseId { get; set; }
    public IReadOnlyCollection<Product> Products => _products.AsReadOnly();
}
