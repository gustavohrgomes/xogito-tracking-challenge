namespace RetailSystem.Domain;

public enum ProductState
{
    /// <summary>
    /// The product is available in the warehouse or store and is ready for sale.
    /// </summary>
    InStock,

    /// <summary>
    /// The product is not available in the warehouse or store, and there are no units in stock.
    /// </summary>
    OutOfStock,

    /// <summary>
    /// The quantity of the product in the warehouse or store is below the predefined threshold, need restocking.
    /// </summary>
    LowStock,

    /// <summary>
    /// The product is currently in the process of being moved from one location to another, 
    /// such as from the main warehouse to a store or between store locations.
    /// </summary>
    InTransit
}
