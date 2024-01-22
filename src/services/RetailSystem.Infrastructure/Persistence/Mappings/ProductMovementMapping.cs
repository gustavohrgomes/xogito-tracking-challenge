using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RetailSystem.Domain;

namespace RetailSystem.Infrastructure.Persistence.Mappings;
internal class ProductMovementMapping : IEntityTypeConfiguration<ProductMovement>
{
    public void Configure(EntityTypeBuilder<ProductMovement> builder)
    {
        builder.ToTable("ProductsMovements");

        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.Origin)
            .IsRequired();

        builder
            .Property(x => x.Destination)
            .IsRequired();

        builder
            .Property(x => x.DispatchUtcDate)
            .ValueGeneratedOnAdd();

        builder
            .Property(x => x.LastUpdatedUtcDate)
            .ValueGeneratedOnUpdate();

        builder.HasIndex(x => x.DispatchUtcDate);
        builder.HasIndex(x => x.ReceivedUtcDate);
        builder.HasIndex(x => x.LastUpdatedUtcDate);
    }
}
