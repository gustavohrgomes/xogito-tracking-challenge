﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Tracking.Domain.Products;
using Warehouse.Tracking.Domain.Warehouses;

namespace Warehouse.Tracking.Infrastructure.Persistence.Mappings;
internal class ProductMapping : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
           .ValueGeneratedNever();

        builder
            .Property(x => x.Name)
            .IsRequired();

        builder
            .Property(x => x.Quantity)
            .IsRequired();

        builder.Property(x => x.State)
            .HasConversion(
                v => v.ToString(),
                v => (ProductState)Enum.Parse(typeof(ProductState), v));

        builder.HasMany<ProductMovement>("_productMovements")
            .WithOne()
            .HasForeignKey("ProductId");

        builder.HasOne<ProductWarehouse>()
            .WithMany()
            .HasForeignKey(x => x.WarehouseId);

        builder.HasIndex(x => x.WarehouseId);
        builder.HasIndex(x => x.StoreId);

        builder.Ignore(x => x.ProductMovements);
    }
}
