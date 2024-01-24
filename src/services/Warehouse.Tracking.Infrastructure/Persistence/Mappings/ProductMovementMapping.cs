using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Tracking.Domain.Products;

namespace Warehouse.Tracking.Infrastructure.Persistence.Mappings;
internal class ProductMovementMapping : IEntityTypeConfiguration<ProductMovement>
{
    public void Configure(EntityTypeBuilder<ProductMovement> builder)
    {
        builder.ToTable("ProductsMovements");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder
            .Property(x => x.SourceId);

        builder
            .Property(x => x.Source);

        builder
            .Property(x => x.DestinationId);

        builder
            .Property(x => x.Destination);

        builder.Property(x => x.State)
            .HasConversion(
                v => v.ToString(),
                v => (MovementState)Enum.Parse(typeof(MovementState), v));

        builder.HasIndex(x => x.DispatchUtcDate);
        builder.HasIndex(x => x.ReceivedUtcDate);
        builder.HasIndex(x => x.LastUpdatedUtcDate);
    }
}
