using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RetailSystem.Domain;

namespace RetailSystem.Infrastructure.Persistence.Mappings;
internal class WarehouseMapping : IEntityTypeConfiguration<Warehouse>
{
    public void Configure(EntityTypeBuilder<Warehouse> builder)
    {
        builder.ToTable("Warehouses");

        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.Location)
            .IsRequired();

        builder.HasMany(x => x.Products);
    }
}
