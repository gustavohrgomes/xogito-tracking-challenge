namespace Warehouse.Tracking.Domain.Products;

public interface IShipProductToStoreService
{
    void SendShippment(ProductShippment shippment);
}

public class ShipProductToStoreService : IShipProductToStoreService
{
    public void SendShippment(ProductShippment shippment)
    {
        throw new NotImplementedException();
    }
}
