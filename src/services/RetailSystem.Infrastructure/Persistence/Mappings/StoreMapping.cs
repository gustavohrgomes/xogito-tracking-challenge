using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RetailSystem.Domain;

namespace RetailSystem.Infrastructure.Persistence.Mappings;
internal class StoreMapping : IEntityTypeConfiguration<Store>
{
    public void Configure(EntityTypeBuilder<Store> builder)
    {
        builder.ToTable("Stores");

        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.Name)
            .IsRequired();

        builder
            .Property(x => x.Location)
            .IsRequired();

        builder.HasMany(x => x.Products);

        builder.HasIndex(x => x.WarehouseId);
    }
}
