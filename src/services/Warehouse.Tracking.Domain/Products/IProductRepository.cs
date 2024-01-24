namespace Warehouse.Tracking.Domain.Products;
public interface IProductRepository
{
    Task<Product> FindByIdAsync(Guid productId, CancellationToken cancellationToken);
    void Insert(Product product);
    void Update(Product product);
}
