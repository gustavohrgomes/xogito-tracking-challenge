namespace Warehouse.Tracking.Domain.Products;

public enum MovementState
{
    None = 0,

    /// <summary>
    /// The product inventory entry in the Warehouse.
    /// </summary>
    InventoryEntry = 10,

    /// <summary>
    /// The product is currently in the process of being moved from one location to another.
    /// </summary>
    InTransit = 20,

    /// <summary>
    /// The product was delivered to it's destination.
    /// </summary>
    Delivered = 30,

    /// <summary>
    /// The product was returned to it's source.
    /// </summary>
    Returned = 40
}
