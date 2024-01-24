using RetailSystem.Domain.Common;

namespace RetailSystem.Domain.Products;

/// <summary>
/// Historic entity for tracking all products movements between Warehouses and Stores
/// </summary>
public class ProductMovement : Entity
{
    private ProductMovement(
        Guid id,
        int productQuantity, 
        Guid sourceId, 
        string source, 
        Guid destinationId, 
        string destination, 
        MovementState state,
        DateTime dispatchUtcDate)
    {
        Id = id;
        ProductQuantity = productQuantity;
        SourceId = sourceId;
        Source = source;
        DestinationId = destinationId;
        Destination = destination;
        State = state;
        DispatchUtcDate = dispatchUtcDate;
    }

    protected ProductMovement()
    { }

    public int ProductQuantity { get; set; }
    public Guid? SourceId { get; set; }
    public string? Source { get; set; }
    public Guid? DestinationId { get; set; }
    public string? Destination { get; set; }
    public string? Description { get; set; }
    public MovementState State { get; set; }
    public DateTime? LastUpdatedUtcDate { get; set; } 
    public DateTime DispatchUtcDate { get; set; }
    public DateTime? ReceivedUtcDate { get; set; }
    public DateTime? InventoryEntryUtcDate { get; set; }

    public static ProductMovement Create(
        int productQuantity,
        Guid sourceId,
        string source,
        Guid destinationId,
        string destination)
    {
        return new ProductMovement(
            Guid.NewGuid(),
            productQuantity, 
            sourceId, 
            source, 
            destinationId, 
            destination,
            MovementState.InTransit,
            DateTime.UtcNow);
    }

    public static ProductMovement EntryMovement(int productQuantity)
    {
        return new ProductMovement
        {
            Id = Guid.NewGuid(),
            ProductQuantity = productQuantity,
            Description = "Product Entry",
            State = MovementState.InventoryEntry,
            ReceivedUtcDate = DateTime.UtcNow,
            InventoryEntryUtcDate = DateTime.UtcNow
        };
    }
}
