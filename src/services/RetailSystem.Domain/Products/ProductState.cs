namespace RetailSystem.Domain.Products;

public enum ProductState
{
    /// <summary>
    /// The product is available in the warehouse or store and is ready for sale.
    /// </summary>
    InStock,

    /// <summary>
    /// The quantity of the product in the warehouse or store is below the predefined threshold, need restocking.
    /// </summary>
    LowStock,

    /// <summary>
    /// The product is not available in the warehouse or store, and there are no units in stock.
    /// </summary>
    OutOfStock
}
