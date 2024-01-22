using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RetailSystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailSystem.Infrastructure.Persistence.Mappings;
internal class ProductMapping : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(x => x.Id);

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

        builder.HasIndex(x => x.WarehouseId);
        builder.HasIndex(x => x.StoreId);
    }
}
