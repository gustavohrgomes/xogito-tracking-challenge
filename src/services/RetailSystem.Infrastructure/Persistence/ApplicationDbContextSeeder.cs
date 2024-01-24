using System.Net.Http.Headers;
using Warehouse.Tracking.Domain.Warehouses;

namespace Warehouse.Tracking.Infrastructure.Persistence;
public class ApplicationDbContextSeeder
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
		try
		{
			await SeedWarehouses(context);
		}
		catch
		{
			throw;
		}
    }

	private static async Task SeedWarehouses(ApplicationDbContext context)
	{
		if (context.Warehouses.Any()) return;

		List<ProductWarehouse> productWarehouses = new();

		ProductWarehouse mainWarehouse = new(Guid.NewGuid(), "Main location", "Main Warehouse");

		productWarehouses.Add(mainWarehouse);

		for (int i = 1; i <= 5; i++)
		{
			productWarehouses.Add(new(Guid.NewGuid(), $"Warehouse location {i}", $"Store Warehouse {i}"));
		}

		await context.Warehouses.AddRangeAsync(productWarehouses);
		await context.SaveChangesAsync();
	}
}
