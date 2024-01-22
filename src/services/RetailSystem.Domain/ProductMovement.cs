using RetailSystem.Domain.Common;

namespace RetailSystem.Domain;

/// <summary>
/// Historic entity for tracking all products movements between Warehouses and Stores
/// </summary>
public class ProductMovement : Entity
{
    public ProductMovement(Guid id)
    {
        Id = id;
    }

    protected ProductMovement()
    { }

    public string Origin { get; set; }
    public string Destination { get; set; }
    public string Description { get; set; }
    public DateTime LastUpdatedUtcDate { get; set; }
    public DateTime DispatchUtcDate { get; set; }
    public DateTime ReceivedUtcDate { get; set; }
}
