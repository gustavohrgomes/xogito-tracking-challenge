using Warehouse.Tracking.Domain.Warehouses;

namespace Warehouse.Tracking.Infrastructure.Persistence.Adpaters.EntityFrameworkCore.Repositories;
public class WarehouseRepository : IWarehouseRepository
{
    public readonly ApplicationDbContext _context;

    public WarehouseRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ProductWarehouse> FindByIdAsync(Guid id) => await _context.Warehouses.FindAsync(id);
}
