using Warehouse.Tracking.Domain.Common;

namespace Warehouse.Tracking.Domain.Warehouses;

public class ProductWarehouse : Entity
{
    public ProductWarehouse(Guid id, string location, string name)
    {
        Id = id;
        Location = location;
        Name = name;
    }

    public string Location { get; private set; }
    public string Name { get; private set; }
}
