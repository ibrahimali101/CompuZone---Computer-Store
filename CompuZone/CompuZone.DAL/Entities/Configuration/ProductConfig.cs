using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompuZone.DAL.Entities.Configuration
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {

        

        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(a => a.ProductID);

            builder.Property(a => a.ProductName).IsRequired().HasMaxLength(100);

            builder.Property(a => a.Description).HasDefaultValue("Nothing Here.");

            builder.Property(a => a.Price).HasColumnType("decimal(18,2)");

            builder.HasOne(a => a.Category)
                   .WithMany(c => c.Products)
                   .HasForeignKey(a => a.CategoryID)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(a => a.Images)
                   .WithOne(i => i.Product)
                   .HasForeignKey(i => i.ProductID)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(a => a.OrderItems)
                .WithOne(i => i.Product)
                .HasForeignKey(i => i.ProductID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
