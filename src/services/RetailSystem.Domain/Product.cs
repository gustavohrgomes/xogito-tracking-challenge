using RetailSystem.Domain.Common;

namespace RetailSystem.Domain;
public class Product : Entity
{
    public Product(Guid id) : base(id)
    {
    }

    public string Name { get; set; }
    public int Quantity { get; set; }
}
