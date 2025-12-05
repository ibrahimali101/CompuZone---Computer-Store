using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompuZone.DAL.Entities.Configuration
{
    public class ProductImageConfig : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.HasKey(pi => pi.ImageID); // Primary Key

            builder.Property(pi => pi.ImageURL)
                   .IsRequired()
                   .HasMaxLength(500); // Assuming a max length for URL

            builder.Property(pi => pi.ImageOrder).HasConversion<int>(); // Ensure ImageOrder is stored as int

            builder.HasOne(pi => pi.Product)
                   .WithMany(p => p.Images)
                   .HasForeignKey(pi => pi.ProductID)
                   .OnDelete(DeleteBehavior.Cascade); // Cascade delete behavior


        }
    }
}
