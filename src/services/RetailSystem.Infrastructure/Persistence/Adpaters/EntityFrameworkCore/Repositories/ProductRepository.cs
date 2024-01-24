using Microsoft.EntityFrameworkCore;
using Warehouse.Tracking.Domain.Products;
using Warehouse.Tracking.Infrastructure.Persistence;

namespace Warehouse.Tracking.Infrastructure.Persistence.Adpaters.EntityFrameworkCore.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Product> FindByIdAsync(Guid productId, CancellationToken cancellationToken)
    {
        var product = await _context.Products.FindAsync(productId, cancellationToken);

        return product!;
    }

    public void Insert(Product product) => _context.Products.Add(product);

    public void Update(Product product) => _context.Products.Update(product);
}
