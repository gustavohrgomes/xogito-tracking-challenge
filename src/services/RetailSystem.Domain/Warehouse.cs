using RetailSystem.Domain.Common;

namespace RetailSystem.Domain;

public class Warehouse : Entity
{
    private readonly List<Product> _products = new();

    public Warehouse(Guid id, string name, string location)
    {
        Id = id;
        Name = name;
        Location = location;
    }

    public Warehouse() { }

    public string Name { get; set; }
    public string Location { get; set; }
    public IReadOnlyCollection<Product> Products => _products.AsReadOnly();

    public void AddProduct(Product product) => _products.Add(product);
}
